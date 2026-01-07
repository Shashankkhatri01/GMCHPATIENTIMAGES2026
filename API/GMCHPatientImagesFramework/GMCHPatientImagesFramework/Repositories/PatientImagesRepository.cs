using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMCHPatientImagesFramework.Repositories
{
    public class PatientImagesRepository : RepositoryBase<PatientImagesDTO, PatientImagesDTO>, IPatientImagesRepository
  {
        public PatientImagesRepository(IConfiguration configuration) : base(configuration)
        {
            ProcedureName = "PatientImages_Curd";
        }

    }
}
