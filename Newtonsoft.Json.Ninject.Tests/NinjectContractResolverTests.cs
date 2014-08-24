namespace Newtonsoft.Json.Ninject.Tests
{
    using FluentAssertions;
    using global::Ninject;
    using Xunit;

    public class NinjectContractResolverTests
    {
        private IKernel kernel = new StandardKernel();

        public NinjectContractResolverTests()
        {
            kernel.Bind<IFoo>().To<Foo>();
            kernel.Bind<BaseFoo>().To<Foo>();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = kernel.Get<NinjectContractResolver>()
            };
        }

        [Fact]
        public void Should_be_able_to_resolve_interface()
        {
            var foo = JsonConvert.DeserializeObject<IFoo>("{\"Bar\":\"Test\"}");

            foo.Bar.Should().Be("Test");
        }

        [Fact]
        public void Should_be_able_to_resolve_abstract_class()
        {
            var foo = JsonConvert.DeserializeObject<BaseFoo>("{\"Bar\":\"Test\"}");

            foo.Bar.Should().Be("Test");
        }

        public interface IFoo
        {
            string Bar { get; set; }
        }

        public abstract class BaseFoo : IFoo
        {
            public abstract string Bar { get; set; }
        }

        public class Foo : BaseFoo, IFoo
        {
            public override string Bar { get; set; }
        }
    }
}
