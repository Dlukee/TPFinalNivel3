using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AplicacionWeb
{
    public static class Validacion
    {
        public static bool validaTextoVacio(object control)
        {
            if(control is TextBox)
            {
                if (string.IsNullOrEmpty(((TextBox)control).Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}