using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Program
{
    public class PropertiesValidator<TEntity> where TEntity : new()
    {
        public void Validate( TEntity entityValues )
        {
            //PropertyInfo[]  properties = typeof(TEntity).GetProperties();
            IEnumerable properties = typeof(TEntity).GetProperties()
                     .Where(p => p.IsDefined(typeof(ColumnAttribute), false))
                     .Select(p => new
                     {
                         PropertyName = p.Name,
                         p.GetCustomAttributes(typeof(ColumnAttribute),
                                 false).Cast<ColumnAttribute>().Single().Name
                     });

            foreach (PropertyInfo currentRealProperty in properties)
            {
                object valor = currentRealProperty.GetValue(entityValues);

                if (valor == null)
                {
                    string exceptionMessage = string.Format("The property {0} of class {1} was not as expected.", currentRealProperty.Name, currentRealProperty.DeclaringType.Name);
                }
            }
        }
    }
}