//-----------------------------------------------------------------------------------------------------
// <copyright file="NinjectCamelCasePropertyNamesContractResolver.cs" company="Scott Xu">
//   Copyright (c) 2014 Scott Xu.
// </copyright>
//-----------------------------------------------------------------------------------------------------

namespace Newtonsoft.Json.Ninject
{
    using System;
    using Newtonsoft.Json.Serialization;
    using global::Ninject;

    /// <summary>
    /// Ninject camelCasePropertyNames contract resolver
    /// </summary>
    public class NinjectCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {        
        /// <summary>
        /// The <see cref="IKernel"/>.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectCamelCasePropertyNamesContractResolver"/> class.
        /// </summary>
        /// <param name="kernel">The <see cref="IKernel"/>.</param>
        public NinjectCamelCasePropertyNamesContractResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Resolve contract by Ninject kernel.
        /// </summary>
        /// <param name="objectType">The contract type.</param>
        /// <returns>The <see cref="JsonObjectContract"/>.</returns>
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract contract = base.CreateObjectContract(objectType);

            contract.DefaultCreator = () => this.kernel.Get(objectType);

            return contract;
        }
    }
}
