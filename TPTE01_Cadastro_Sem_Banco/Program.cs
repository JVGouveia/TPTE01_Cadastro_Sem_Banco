using Microsoft.OpenApi.Models;
using TPTE01_Cadastro_Sem_Banco.Models;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Lista em memória para armazenar os produtos
List<ProdutoModel> produtos = new List<ProdutoModel>
{
    new ProdutoModel { ProdutoId = 1, Nome = "Product 1", Descricao = "Description of Product 1", PrecoUnitario = 10.99m, Quantidade = 100 },
    new ProdutoModel { ProdutoId = 2, Nome = "Product 2", Descricao = "Description of Product 2", PrecoUnitario = 20.49m, Quantidade = 50 },
    new ProdutoModel { ProdutoId = 3, Nome = "Product 3", Descricao = "Description of Product 3", PrecoUnitario = 15.79m, Quantidade = 200 }
};

// Rota GET para obter todos os produtos
app.MapGet("/produtos", () => produtos);

// Rota GET para obter um produto pelo Id
app.MapGet("/produtos/{id}", (int id) =>
{
    var product = produtos.FirstOrDefault(p => p.ProdutoId == id);
    if (product == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(product);
});

// Rota POST para criar um novo produto
app.MapPost("/produtos", (ProdutoModel produto) =>
{
    produto.ProdutoId = produtos.Count + 1;
    produtos.Add(produto);
    return Results.Created($"/products/{produto.ProdutoId}", produto);
});

// Rota PUT para atualizar um produto existente
app.MapPut("/produtos/{id}", (int id, ProdutoModel updatedProduct) =>
{
    var existingProduct = produtos.FirstOrDefault(p => p.ProdutoId == id);
    if (existingProduct == null)
    {
        return Results.NotFound();
    }

    existingProduct.Nome = updatedProduct.Nome;
    existingProduct.Descricao = updatedProduct.Descricao;
    existingProduct.PrecoUnitario = updatedProduct.PrecoUnitario;
    existingProduct.Quantidade = updatedProduct.Quantidade;

    return Results.Ok(existingProduct);
});

// Rota DELETE para excluir um produto
app.MapDelete("/produtos/{id}", (int id) =>
{
    var existingProduct = produtos.FirstOrDefault(p => p.ProdutoId == id);
    if (existingProduct == null)
    {
        return Results.NotFound();
    }

    produtos.Remove(existingProduct);
    return Results.NoContent();
});

app.Run();