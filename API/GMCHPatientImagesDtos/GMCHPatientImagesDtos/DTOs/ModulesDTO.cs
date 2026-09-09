using GMCHPatientImagesDtos.Attributes;

namespace GMCHPatientImagesDtos.DTOs
{
    public class ModulesDTO : BaseDTO
    {
        [IgnoreParam]
        public long ModuleId { get; set; }
        [IgnoreParam]
        public string ModuleCode { get; set; }
        [IgnoreParam]
        public string ModuleName { get; set; }
        [IgnoreParam]
        public string ModuleSubName { get; set; }
        [IgnoreParam]
        public string ModuleDescription { get; set; }
        [IgnoreParam]
        public string ModuleIcon { get; set; }
        [IgnoreParam]
        public string ModuleColor { get; set; }
        [IgnoreParam]
        public string ModuleType { get; set; }
        [IgnoreParam]
        public string ModuleUrl { get; set; }
        [IgnoreParam]
        public string ModuleStatus { get; set; }
        [IgnoreParam]
        public string ModuleCategory { get; set; }
        [IgnoreParam]
        public string ModuleTag { get; set; }
        [IgnoreParam]
        public string RoleIds { get; set; }
    }
}
