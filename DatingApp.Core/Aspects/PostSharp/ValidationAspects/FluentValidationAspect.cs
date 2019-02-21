using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FluentValidation;
using PostSharp.Aspects;

namespace DatingApp.Core.Aspects.PostSharp.ValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private readonly Type _validationType;
        public FluentValidationAspect(Type _validationType)
        {
            this._validationType = _validationType;

        }

       public override void OnEntry(MethodExecutionArgs args)
       {
            var validator = (IValidator)Activator.CreateInstance(_validationType);
            var entityType = _validationType.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(t=>t.GetType()==entityType);

            foreach (var item in entities)
            {
                ValidatorTool.FluentValidate(validator,item);
            }
       }
    }
}