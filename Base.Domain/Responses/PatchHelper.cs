namespace Base.Domain.Responses;

public static class PatchHelper
{
    public static void ApplyPath<TTarget, TPatch>(TTarget target, TPatch patch)
    {
        var targetType = typeof(TTarget);
        var patchType = typeof(TPatch);
        
        var patchProperties = patchType.GetProperties();

        foreach (var patchProp in patchProperties)
        {
            var patchValue = patchProp.GetValue(patch);

            if (patchValue != null)
            {
                var targetProp = targetType.GetProperty(patchProp.Name);

                if (targetProp != null && patchProp.CanWrite)
                {
                    targetProp.SetValue(target, patchValue);
                }
            }
        }
    }
}