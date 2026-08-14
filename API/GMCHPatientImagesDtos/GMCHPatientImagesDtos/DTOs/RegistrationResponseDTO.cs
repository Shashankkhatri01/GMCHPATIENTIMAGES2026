namespace GMCHPatientImagesDtos.DTOs
{
    public class RegistrationResponseDTO : BaseDTO
    {
        public long LoginId { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Doctors { get; set; }
    }
}
