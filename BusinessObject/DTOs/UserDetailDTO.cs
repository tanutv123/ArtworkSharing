using BusinessObject.Entities;

namespace BusinessObject.DTOs;

public class UserDetailDTO
{
    public string userImageUrl { get; set; }
    public string Name { get; set; }
    public string emailAddress { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
   
}