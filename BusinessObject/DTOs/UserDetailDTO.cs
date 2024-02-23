using BusinessObject.Entities;

namespace BusinessObject.DTOs;

public class UserDetailDTO
{
    private UserImage? userImage { get; set; }
    private string Name { get; set; }
    private string emailAddress { get; set; }
    private string Description { get; set; }
    private string PhoneNumber { get; set; }
    private string currentPassword { get; set; }
    private string newPassword { get; set; }
}