using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_Retrieval
{
    class QueryBuilder1
    {
        StringBuilder finalQuery;
        private string inside = null;
        private string outside;
        public List<string> GetQuery(string userQuery)
        {
            char[] delimiterChars = { ' ', ';' };
            List<string> temp = userQuery.Split(delimiterChars).ToList<string>();
            List<Dictionary<string, int>> dic = new List<Dictionary<string, int>>();
            
            if(userQuery.Contains("(") && userQuery.Contains(")"))
            {
                var start = userQuery.IndexOf('(') + 1;
                var end  = userQuery.IndexOf(')');
                inside = userQuery.Substring(start,end - start);
                //outside = //fds  + inside
            }
            
            return null;
        }
    }
}
