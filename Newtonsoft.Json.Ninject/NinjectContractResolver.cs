//-----------------------------------------------------------------------------------------------------
// <copyright file="NinjectContractResolver.cs" company="Scott Xu">
//   Copyright (c) 2014 Scott Xu.
// </copyright>
//-----------------------------------------------------------------------------------------------------

namespace Newtonsoft.Json.Ninject
{
    using System;
    using Newtonsoft.Json.Serialization;
    using global::Ninject;

    /// <summary>
    /// Ninject contract resolver
    /// </summary>
    public class NinjectContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// The <see cref="IKernel"/>.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectContractResolver"/> class.
        /// </summary>
        /// <param name="kernel">The <see cref="IKernel"/>.</param>
        public NinjectContractResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        /// <summary>
        /// Resolve contract by kernel.
        /// </summary>
        /// <param name="objectType">The contract type.</param>
        /// <returns>The <see cref="JsonObjectContract"/>.</returns>
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract contract = base.CreateObjectContract(objectType);

            contract.DefaultCreator = () => this.kernel.TryGet(objectType);

            return contract;
        }
    }
}
