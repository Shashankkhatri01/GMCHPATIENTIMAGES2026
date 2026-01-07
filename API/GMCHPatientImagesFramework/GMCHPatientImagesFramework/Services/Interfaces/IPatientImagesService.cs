using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GMCHPatientImagesDtos.DTOs;

namespace GMCHPatientImagesFramework.Services.Interfaces
   
{
    public interface IPatientImagesService
    { 
        //Insert
        Task<ReturnObject<long>> Insert(PatientImagesDTO patientImagesDTO); 
        //Get All Data
        Task<ReturnObject<List<PatientImagesDTO>>> GetAll(PatientImagesDTO patientImagesDTO);
        //Delete Data
        Task<ReturnObject<long>> Delete(PatientImagesDTO patientImagesDTO);

  }
}
