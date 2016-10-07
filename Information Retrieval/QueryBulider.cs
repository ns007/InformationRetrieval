using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Information_Retrieval
{
    class QueryBulider
    {
        StringBuilder finalQuery;
        Dictionary<string, Func<string, StringBuilder, StringBuilder>> operators;
        public QueryBulider()
        {
            finalQuery = new StringBuilder("SELECT * FROM info_retrieval_db.words_tbl WHERE 1=1 ");
            operators = new Dictionary<string, Func<string, StringBuilder, StringBuilder>>();
            operators.Add("AND", (andOperator));
            operators.Add("OR", orOperator);
            operators.Add("NOT", notOperator);
        }

        public string GetQuery(string userQuery)
        {
            List<string> queryTokens = GetQueryTokens(userQuery);
            foreach (string queryToken in queryTokens)
            {
                if (!operators.ContainsKey(queryToken))
                    finalQuery.Append("AND word = '" + queryToken + "'");
                else
                    operators[queryToken]("",finalQuery);
            }
            //AND

            return null;
        }

        public List<string> GetQueryTokens(string userQuery)
        {
            char[] delimiterChars = { ' ', ';' };
            userQuery = userQuery.Replace("(", ";(;").Replace(")", ";);");
            List<string> splitedQuery = userQuery.Split(delimiterChars).ToList<string>();
            splitedQuery.RemoveAll(isEmptyString);
            return splitedQuery;
        }

        private bool isEmptyString(string s)
        {
            return s.Equals("");
        }

        private StringBuilder andOperator(string after, StringBuilder finalQuery)
        {
            finalQuery.Append(" AND " + after);
            return finalQuery;
        }

        private StringBuilder orOperator(string after, StringBuilder finalQuery)
        {
            return finalQuery;
        }

        private StringBuilder notOperator(string after, StringBuilder finalQuery)
        {
            return finalQuery;
        }
    }
}
