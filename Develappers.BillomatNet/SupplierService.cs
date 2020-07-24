﻿using System;
using Develappers.BillomatNet.Api.Net;

namespace Develappers.BillomatNet
{
    public class SupplierService : ServiceBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="SupplierService"/>.
        /// </summary>
        /// <param name="configuration">The service configuration.</param>
        public SupplierService(Configuration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="SupplierService"/> for unit tests.
        /// </summary>
        /// <param name="httpClientFactory">The function which creates a new <see cref="IHttpClient" /> implementation.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parameter is null.</exception>
        internal SupplierService(Func<IHttpClient> httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
