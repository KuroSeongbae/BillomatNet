﻿using System;
using Develappers.BillomatNet.Api;
using Develappers.BillomatNet.Helpers;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Api.Net;
using Develappers.BillomatNet.Queries;
using Tax = Develappers.BillomatNet.Types.Tax;

namespace Develappers.BillomatNet
{
    public class TaxService : ServiceBase, IEntityService<Tax, TaxFilter>
    {
        /// <summary>
        /// Creates a new instance of <see cref="TaxService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public TaxService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="TaxService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal TaxService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }

        /// <summary>
        /// Retrieves a list of taxes.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the list of taxes.
        /// </returns>
        public async Task<Types.PagedList<Tax>> GetListAsync(CancellationToken token = default)
        {
            var jsonModel = await GetListAsync<TaxListWrapper>("/api/taxes", null, token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task<Types.PagedList<Tax>> IEntityService<Tax, TaxFilter>.GetListAsync(Query<Tax, TaxFilter> query, CancellationToken token)
        {
            // TODO: implement implicitly and make public
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a tax item by it's ID.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the unit.
        /// </returns>
        public async Task<Tax> GetByIdAsync(int id, CancellationToken token = default)
        {
            var jsonModel = await GetItemByIdAsync<TaxWrapper>($"/api/taxes/{id}", token).ConfigureAwait(false);
            return jsonModel.ToDomain();
        }

        Task IEntityService<Tax, TaxFilter>.DeleteAsync(int id, CancellationToken token)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates an tax.
        /// </summary>
        /// <param name="model">The tax object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result returns the newly created tax with the ID.
        /// </returns>
        public async Task<Tax> CreateAsync(Tax model, CancellationToken token = default)
        {
            if (model == null || model.Name == "" || model.Name == null)
            {
                throw new ArgumentException();
            }
            if (model.Id != 0)
            {
                throw new ArgumentException("invalid tax id", nameof(model));
            }

            var wrappedModel = new TaxWrapper
            {
                Tax = model.ToApi()
            };
            var result = await PostAsync("/api/taxes", wrappedModel, token);

            return result.ToDomain();
        }

        Task<Tax> IEntityService<Tax, TaxFilter>.EditAsync(Tax model, CancellationToken token)
        {
            // TODO: implement implicitly and make public
            throw new System.NotImplementedException();
        }
    }
}
