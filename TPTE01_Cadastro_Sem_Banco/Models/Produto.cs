namespace TPTE01_Cadastro_Sem_Banco.Models;

public class ProdutoModel
{
    public int ProdutoId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) {
            return false;
        }

        ProdutoModel produto = (ProdutoModel)obj;
        return produto.ProdutoId.Equals(this.ProdutoId);
    }
}