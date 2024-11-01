public class PhotoPublicationDTO
{
    public int Id { get; set; }           // ID de la relación
    public int IdPublication { get; set; } // ID de la publicación
    public int IdPhoto { get; set; }       // ID de la foto
}

public class PhotoPublicationPostPutDTO
{
    public int IdPublication { get; set; } // ID de la publicación
    public int IdPhoto { get; set; }       // ID de la foto
}
