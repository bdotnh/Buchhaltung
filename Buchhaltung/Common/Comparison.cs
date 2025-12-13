using System;

namespace Buchhaltung.Common
{
    public class Comparison
    {
        public virtual void ShowComparison(){}
        protected virtual void CalculateDiffs(){}

        protected virtual float GetDifference(float amount1, float amount2)
        {
            double value = 0.0f;
            if (amount1 != amount2 && amount1 < amount2)
            {
                value = amount2 - amount1;
            }
            else if (amount1 != amount2 && amount1 > amount2)
            {
                value = amount1 - amount2;
            }
            value = Math.Round(value, 2);
            float res = (float)value;

            return res;
        }

        protected virtual float GetPercentageChange(float startValue, float endValue)
        {
            if (startValue == 0.0 || endValue == 0.0)
            {
                if (startValue == 0.0)
                {
                    return endValue;
                } 
                else
                {
                    return startValue;
                }
            }
            if (startValue < 0 && endValue < 0)
            {
                startValue = Math.Abs(startValue);
                endValue = Math.Abs(endValue);
            }
            double diff = 100 * (endValue - startValue) / startValue;
            double diffRounded = Math.Round(diff, 2);
            if (startValue < 0 && endValue > 0)
            {
                diffRounded = Math.Abs(diffRounded);
            }
            float res = (float)diffRounded;

            return res;
        }
    }
}