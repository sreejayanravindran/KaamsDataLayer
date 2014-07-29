using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;

namespace Kaams.Entity
{
    public class KaamsGenericCommon<T> where T : class
    {
      protected  MainContext mainContext = new MainContext();

      protected  string ReturnStringWithFormat(string[] parameters, string storedProcedure)
        {
            string items = null;
            for (int i = 0; i < parameters.Count(); i++)
            {
                // items = items + "{" + i.ToString() + "}";


                items = items + "{" + i.ToString() + "}";
                if ((parameters.Count() > 0) && (i != parameters.Count() - 1))
                {
                    items = items + ",";
                }
            }

            return storedProcedure + " " + items;
        }

      protected IEnumerable<Object> GetItemListWithParameters(string procedure)
        {
            return mainContext.Database.SqlQuery<Object>(procedure);

        }

      protected string GetOutputWithOutputParamets()
        {
            List<SqlParameter> args = new List<SqlParameter>();

            return "";
        }
     protected Dictionary<List<SqlParameter>, string> ReturnFormatedStoredProcedure(string storedProcedure, Dictionary<string, string> dictionaryParameters, string outPutParameter, bool needOutPutParameter)
      {
          List<SqlParameter> sqlParameterCollection = new List<SqlParameter>();
          string parameters = null;
          if (needOutPutParameter == true)
          {
              parameters = "@" + outPutParameter + " OUT,";

              sqlParameterCollection.Add(new SqlParameter
              {
                  ParameterName = outPutParameter,
                  Value = "",
                  Direction = ParameterDirection.Output,
                  SqlDbType = SqlDbType.NVarChar,
                  Size = 5000
                  //, SqlDbType = SqlDbType.Int
              });


          }
          //adding parameters
          foreach (var pair in dictionaryParameters)
          {
              sqlParameterCollection.Add(new SqlParameter
              {
                  ParameterName = pair.Key,
                  Value = pair.Value,

                  Direction = ParameterDirection.Input
              });
              parameters = parameters + "@" + pair.Key + ",";

          }

          parameters = parameters != null ? parameters.Remove(parameters.Length - 1) : parameters;
          // return storedProcedure + " " + parameters;
          return new Dictionary<List<SqlParameter>, string>() { { sqlParameterCollection, storedProcedure + " " + parameters } };
          //

      }

    }
}