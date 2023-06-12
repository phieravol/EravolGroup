﻿using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.Repositories.Images;
using Eravol.WebApi.ViewModels.ServiceImages.Freelancers;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Eravol.WebApi.Repositories.ServiceImages.Freelancers
{

	public class ServiceImagesRepository : IServiceImagesRepository
	{
		private readonly EravolUserWebApiContext context;
		private readonly IFileStorageService storageService;
		private const string USER_CONTENT_FOLDER_NAME = "user-content";

		public ServiceImagesRepository(EravolUserWebApiContext context, IFileStorageService storageService)
		{
			this.context = context;
			this.storageService = storageService;
		}

		public async Task<List<ServiceImage>> CreateServiceImages(string serviceCode, List<IFormFile>? serviceImages)
		{
			if (serviceImages == null)
			{
				return null;
			}

			List<ServiceImage> serviceImagesList = new List<ServiceImage>();

			foreach (var image in serviceImages)
			{
				ServiceImage ServiceImage = new ServiceImage()
				{
					DateCreated = DateTime.Now,
					ServiceCode = serviceCode,
					isThumbnail = false,
					ServiceImageSize = image.Length
				};

				ServiceImage.ImageName = await SaveFile(image);
				ServiceImage.ServiceImagePath = "/" + USER_CONTENT_FOLDER_NAME + "/" + ServiceImage.ImageName;
				
				serviceImagesList.Add(ServiceImage);
			}

			try
			{
				context.ServicesImages.AddRange(serviceImagesList);
				await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return serviceImagesList;
		}

		private async Task<string> SaveFile(IFormFile file)
		{
			var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
			await storageService.SaveFileAsync(file.OpenReadStream(), fileName);
			return fileName;
		}

		public async Task<List<ServiceImage>> GetSeviceImagesByCodeAsync(string serviceCode)
		{
			List<ServiceImage> servicesImages = new List<ServiceImage>();
			try
			{
				servicesImages = await context.ServicesImages
					.Where(x => x.ServiceCode == serviceCode)
					.ToListAsync();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return servicesImages;
		}
	}
}