public class ShoppingCartDTO
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int IdPublication { get; set; }
    public int Quantity {get;set;}
    public decimal Total{get;set;}
    public string PublicationTitle { get; set; } // Título de la publicación
    public decimal PublicationPrice { get; set; } // Precio de la publicación
    public List<ShoppingCartItemDTO> Items { get; set; } = new List<ShoppingCartItemDTO>();
}


public class ShoppingCartPostDTO
{
    public int IdPublication { get; set; }
    public int Quantity {get;set;}
}
