using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Runtime.InteropServices;



namespace Kaams.Entity
{
    public class KaamsGenericRepository<T> : KaamsGenericCommon<T> where T : class
    {   
        /*Return Objects ------start----*/
         public IEnumerable<T> GetItemList(string procedure)
         {

           //  var x = mainContext.Database.SqlQuery<dynamic>(procedure);
             return mainContext.Database.SqlQuery<T>(procedure);
          
         }
         //get items with parameters
         public IEnumerable<T> GetItemListWithParameters(string[] parameters, string storedProcedure)
         {
             storedProcedure = ReturnStringWithFormat(parameters, storedProcedure);
             return mainContext.Database.SqlQuery<T>(storedProcedure, parameters.ToArray());

         }
         /*Return Objects ------end----*/
         /*execute ------start----*/
         public string ExecuteCommandWithReturnParameter(string storedProcedure, Dictionary<string, string> dictionaryWithValueandParameters, string outPutParameter)
         {
             var formatedStoredProcedure = ReturnFormatedStoredProcedure(storedProcedure, dictionaryWithValueandParameters, outPutParameter, true);
             mainContext.Database.ExecuteSqlCommand(formatedStoredProcedure.First().Value, formatedStoredProcedure.First().Key.ToArray());
           return formatedStoredProcedure.First().Key.First().Value.ToString();
          
                 //(string)sqlParameterCollection[0].Value;
         }



         public void ExecuteCommandWithoutReturnParameter(string storedProcedure,Dictionary<string, string> dictionaryWithValueandParameters)
         {
             var formatedStoredProcedure = ReturnFormatedStoredProcedure(storedProcedure, dictionaryWithValueandParameters, null, false);

             mainContext.Database.ExecuteSqlCommand(formatedStoredProcedure.First().Value, formatedStoredProcedure.First().Key.ToArray());
                 //sqlParameterCollection.ToArray());
             //return (string)sqlParameterCollection[0].Value;
         }


         public void ExecuteCommandWithoutReturnParameter(string storedProcedure, string[] parameters)
         {
            var total = parameters.Count();
             //need to add the output parameter here 
             storedProcedure = ReturnStringWithFormat(parameters, storedProcedure);
           // storedProcedure = storedProcedure + " " + items;
            mainContext.Database.ExecuteSqlCommand(storedProcedure, parameters.ToArray());

         }
         /*execute ------end----*/
      


    }
}