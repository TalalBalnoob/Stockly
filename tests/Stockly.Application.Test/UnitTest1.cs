using Stockly.Infrastructure.Test;

namespace Stockly.Application.Test;

public class ProductServiceTests {
    private FakeProductRepository _productRepo = null!;

    [SetUp]
    public void Setup() {
        _productRepo = new FakeProductRepository();
    }

    [Test]
    public async Task Test1() {
    }
}