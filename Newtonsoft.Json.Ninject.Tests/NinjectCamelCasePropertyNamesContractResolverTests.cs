namespace Newtonsoft.Json.Ninject.Tests
{
    using FluentAssertions;
    using global::Ninject;
    using Xunit;

    public class NinjectCamelCasePropertyNamesContractResolverTests
    {
        private IKernel kernel = new StandardKernel();

        public NinjectCamelCasePropertyNamesContractResolverTests()
        {
            kernel.Bind<IFoo>().To<Foo>();
            kernel.Bind<BaseFoo>().To<Foo>();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = kernel.Get<NinjectCamelCasePropertyNamesContractResolver>()
            };
        }

        [Fact]
        public void Should_be_able_to_resolve_interface()
        {
            var foo = JsonConvert.DeserializeObject<IFoo>("{\"Bar\":\"Test\"}");

            var jsonFoo = JsonConvert.SerializeObject(foo);

            foo.Bar.Should().Be("Test");
            jsonFoo.Should().Be("{\"bar\":\"Test\"}");
        }

        [Fact]
        public void Should_be_able_to_resolve_abstract_class()
        {
            var foo = JsonConvert.DeserializeObject<BaseFoo>("{\"Bar\":\"Test\"}");
            var jsonFoo = JsonConvert.SerializeObject(foo);

            foo.Bar.Should().Be("Test");
            jsonFoo.Should().Be("{\"bar\":\"Test\"}");
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
