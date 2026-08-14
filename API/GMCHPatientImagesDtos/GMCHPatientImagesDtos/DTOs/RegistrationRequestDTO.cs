namespace GMCHPatientImagesDtos.DTOs
{
    public class RegistrationRequestDTO : BaseDTO
    {
        public long LoginId { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public string Doctors { get; set; }
    }
}
