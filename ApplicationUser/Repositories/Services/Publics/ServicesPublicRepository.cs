using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;
using Eravol.WebApi.ViewModels.Services.Public;
using Microsoft.EntityFrameworkCore;

namespace Eravol.WebApi.Repositories.Services.Publics
{
	public class ServicesPublicRepository : IServicesPublicRepository
	{
		private const string ANY_TYPE = "anyType";
		private const string HOURLY_TYPE = "hourPrice";
		private const string FIX_TYPE = "fixPrice";
		private const decimal FILTER_LIMIT = 150;

		private readonly EravolUserWebApiContext context;

		public ServicesPublicRepository(EravolUserWebApiContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// Get a service by service code
		/// </summary>
		/// <param name="serviceCode"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceViewModel> GetDetailServiceAsync(string? serviceCode)
        {
            var query = from s in context.Services
                        join u in context.AppUsers on s.UserId equals u.Id
                        join c in context.Categories on s.CategoryId equals c.CategoryId into usJoined
                        from c in usJoined.DefaultIfEmpty()
                        join status in context.ServiceStatuses on s.ServiceStatusId equals status.ServiceStatusId into statusJoined
                        from status in statusJoined.DefaultIfEmpty()
						where s.ServiceCode == serviceCode
                        select new
                        {
                            s,
                            u,
                            c,
                            status
                        };
            ServiceViewModel? service = await query.Select(x => new ServiceViewModel()
            {
                ServiceCode = x.s.ServiceCode,
                ServiceTitle = x.s.ServiceTitle,
                CategoryId = x.c.CategoryId,
                CategoryImage = x.c.CategoryImage,
                CategoryName = x.c.CategoryName,
                FreelancerName = $"{x.u.FirstName} {x.u.LastName}",
                Price = x.s.Price,
				PriceType = x.s.PriceType,
                ServiceAuthor = $"{x.u.FirstName} {x.u.LastName}",
                ServiceDetails = x.s.ServiceDetails,
                ServiceIntro = x.s.ServiceIntro,
                ServiceStatusId = x.s.ServiceStatusId,
                ServiceStatusName = x.status.ServiceStatusName,
                TotalClients = x.s.TotalClients,
                TotalStars = x.s.TotalStars,
                UserId = x.s.UserId
            }).FirstOrDefaultAsync();

			return service;
        }

        /// <summary>
        /// Filter service paging
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ServiceViewModel>> GetPublicServices(PublicServicePagingRequest? request)
		{
			List<ServiceViewModel> services = new List<ServiceViewModel>();
			try
			{
				//Init query service left join with AppUser & Category & ServiceStatus model
				var query = from s in context.Services
							join u in context.AppUsers on s.UserId equals u.Id
							join c in context.Categories on s.CategoryId equals c.CategoryId into usJoined
							from c in usJoined.DefaultIfEmpty()
							join status in context.ServiceStatuses on s.ServiceStatusId equals status.ServiceStatusId into statusJoined
							from status in statusJoined.DefaultIfEmpty()
							select new
							{
								s, u, c, status
							};

				//Filter service by keywork if user search service
				if (!string.IsNullOrEmpty(request.SearchTerm))
				{
					query = query.Where(x => x.s.ServiceTitle.Contains(request.SearchTerm) || x.s.ServiceTitle.Contains(request.SearchTerm));
				}

				//Filter service by categories id
				if (request.categoryFilters != null)
				{
					query = query.Where(x => request.categoryFilters.Contains((int)x.s.CategoryId));
				}

				//Filter service by service statuses id
				if (request.serviceStatusFilters != null)
				{
					query = query.Where(x => request.serviceStatusFilters.Contains(x.s.ServiceStatusId));
				}

				//Filter service base on price type
				switch (request.PriceType)
				{
					case HOURLY_TYPE:
						{
							//filter service if price type is Hourly
							query = query.Where(x => x.s.PriceType.Equals(HOURLY_TYPE));
							break;
						}
					case FIX_TYPE:
						{
							//filter service if price type is Fixed
							query = query.Where(x => x.s.PriceType.Equals(FIX_TYPE));
							break;
						}
					default:
						//Not filter by price type if price type is anyType
						break;
				}

				/* filter service by price value when maxFilter reach out limit */
				if (request.MaxPrice == FILTER_LIMIT)
				{
					query = query.Where(x => x.s.Price >= FILTER_LIMIT);
				}
				else
				{
					//filter service by price value in range filter limit
					query = query.Where(x => x.s.Price >= request.MinPrice && x.s.Price <= request.MaxPrice);
				}

				services = await query.Select(x => new ServiceViewModel()
				{
					ServiceCode = x.s.ServiceCode,
					ServiceTitle = x.s.ServiceTitle,
					CategoryId = x.c.CategoryId,
					CategoryImage = x.c.CategoryImage,
					CategoryName = x.c.CategoryName,
					FreelancerName = $"{x.u.FirstName} {x.u.LastName}",
					Price = x.s.Price,
					PriceType = request.PriceType,
					ServiceAuthor = $"{x.u.FirstName} {x.u.LastName}",
					ServiceDetails = x.s.ServiceDetails,
					ServiceIntro = x.s.ServiceIntro,
					ServiceStatusId = x.s.ServiceStatusId,
					ServiceStatusName = x.status.ServiceStatusName,
					TotalClients = x.s.TotalClients,
					TotalStars = x.s.TotalStars,
					UserId = x.s.UserId
				}).ToListAsync();
				//Set limit items in each pages
				request.PageSize = 5;
				//Set totoal pages for paging
				request.TotalPages = (int)Math.Ceiling(services.Count() / (double)request.PageSize);
				
				//Get Services in each pages
				services = services.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
				//Set Items in each pages
				request.Items = services;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			return services;
		}
	}
}
