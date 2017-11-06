using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Core.ErrorsApiHandling 
{
    public class GrupoTheraErrors
    { 
        public String GetErrorCode()
        {
            return this.errorCode;
        }

        public string errorMessage { get; private set; } 

        private string errorCode;

        public static readonly GrupoTheraErrors ELEMENT_NOT_FOUND = new GrupoTheraErrors() {
            errorCode = "GT0002",
            errorMessage = "GT0002 : Element with that property doesn't exists in the database"
        };

        public static readonly GrupoTheraErrors LIST_EMPTY = new GrupoTheraErrors()
        {
            errorCode = "GT0003",
            errorMessage = "GT0003 : The list of the Elements of this entity not contain any element"
        }; 
    } 
}
