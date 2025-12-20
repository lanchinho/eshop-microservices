using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductResult(Guid Id);

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
	: ICommand<CreateProductResult>
{
}

internal class CreateProductCommandHandler
	: ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
        var product = new Product
        {
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
            Name = command.Name,
            //save to db
            Id = Guid.NewGuid()
        };

        return new CreateProductResult(product.Id);
	}
}
