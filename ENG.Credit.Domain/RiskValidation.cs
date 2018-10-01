using ENG.Fuzzy;
using ENG.Fuzzy.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Credit.Domain
{
    public class RiskValidation
    {
        public float Validate(decimal salary, int employmentTime, int age, int dependents, decimal amount)
        {
            var conceptsList = this.GetConcepts(salary, employmentTime, age, dependents, amount);

            var ageConcept = conceptsList[concepts.Age];
            var dependentsConcept = conceptsList[concepts.Dependents];
            var salaryConcept = conceptsList[concepts.Salary];
            var employmemtTimeConcept = conceptsList[concepts.EmploymentTime];
            var amountConcept = conceptsList[concepts.Amount];
            var riskConcept = conceptsList[concepts.Risk];

            var salaryGoAhead = Operator.Or(salaryConcept.Fuzzify(level.High), salaryConcept.Fuzzify(level.VeryHigh));
            var salaryLowOrMedium = Operator.Or(salaryConcept.Fuzzify(level.Low), salaryConcept.Fuzzify(level.Medium));
            var amountNotHighOrVeryHigh = Operator.Not(Operator.Or(amountConcept.Fuzzify(level.High), amountConcept.Fuzzify(level.VeryHigh)));
            var salaryLowOrMediumamountNotHighOrVeryHigh = Operator.And(salaryLowOrMedium, amountNotHighOrVeryHigh);
            var salaryLowOrMediumamountNotHighOrVeryHighDependentsNotHigh = Operator.And(salaryLowOrMediumamountNotHighOrVeryHigh, Operator.Not(dependentsConcept.Fuzzify(level.High)));
            salaryGoAhead = Operator.Or(salaryGoAhead, salaryLowOrMediumamountNotHighOrVeryHighDependentsNotHigh);
            var salaryVerify = Operator.And(salaryGoAhead, Operator.Not(employmemtTimeConcept.Fuzzify(level.High)));
            var salaryDeny = Operator.And(Operator.Not(salaryGoAhead), Operator.Not(salaryVerify));

            var ageGoAhead = Operator.Or(ageConcept.Fuzzify(level.Medium), ageConcept.Fuzzify(level.High));
            var ageVerify = ageConcept.Fuzzify(level.VeryHigh);
            var ageDeny = ageConcept.Fuzzify(level.Low);
               
            var dependentsGoAhead = Operator.Or(salaryGoAhead, Operator.And(salaryVerify, dependentsConcept.Fuzzify(level.Low)));
            var dependentsVerify = Operator.And(salaryVerify, Operator.Not(dependentsConcept.Fuzzify(level.High)));
            var dependentsDeny = Operator.And(Operator.Not(dependentsGoAhead), Operator.Not(dependentsVerify));

            riskConcept.Functions.FirstOrDefault(t => t.Level == level.Low).Fuzzy = Operator.And(salaryGoAhead, Operator.And(ageGoAhead, dependentsGoAhead));
            riskConcept.Functions.FirstOrDefault(t => t.Level == level.Medium).Fuzzy = Operator.Or(dependentsVerify, Operator.Or(salaryVerify, ageVerify));
            riskConcept.Functions.FirstOrDefault(t => t.Level == level.High).Fuzzy = Operator.Or(salaryDeny, Operator.Or(ageDeny, dependentsDeny));

            var risk = new Risk
            {
                High = riskConcept.GetValue(level.High),
                Medium = riskConcept.GetValue(level.Medium),
                Low = riskConcept.GetValue(level.High),
                Value = riskConcept.Defuzzify(0.001f)
            };

            return risk.Value;
        }

        public Dictionary<concepts, Concept> GetConcepts(decimal salary, int employmentTime, int age, int dependents, decimal amount)
        {
            var risk = new Concept();
            risk.Functions.Add(new Descida(level.Low, 0, 25, 40));
            risk.Functions.Add(new Triangular(level.Medium, 35, 55, 70));
            risk.Functions.Add(new Subida(level.High, 65, 85, 100));

            var salaryConcept = new Concept((float)salary);
            salaryConcept.Functions.Add(new Descida(level.Low, 0, 500, 1200));
            salaryConcept.Functions.Add(new Trapezoidal(level.Medium, 800, 1600, 2000, 2500));
            salaryConcept.Functions.Add(new Trapezoidal(level.High, 2500, 3300, 4500, 5000));
            salaryConcept.Functions.Add(new Subida(level.VeryHigh, 5000, 8000, 12100));

            var employmentTimeConcept = new Concept(employmentTime);
            employmentTimeConcept.Functions.Add(new Descida(level.Low, 0, 3, 6));
            employmentTimeConcept.Functions.Add(new Triangular(level.Medium, 6, 12, 16));
            employmentTimeConcept.Functions.Add(new Subida(level.High, 16, 24, 38));

            var ageConcept = new Concept(age);
            ageConcept.Functions.Add(new Descida(level.Low, 0, 15, 21));
            ageConcept.Functions.Add(new Triangular(level.Medium, 18, 25, 28));
            ageConcept.Functions.Add(new Trapezoidal(level.High, 28, 35, 55, 70));
            ageConcept.Functions.Add(new Subida(level.VeryHigh, 65, 80, 110));

            var dependentsConcept = new Concept(dependents);
            dependentsConcept.Functions.Add(new Descida(level.Low, 0, 0, 4));
            dependentsConcept.Functions.Add(new Triangular(level.Medium, 3, 5, 6));
            dependentsConcept.Functions.Add(new Subida(level.High, 5, 6, 14));

            var amountConcept = new Concept((float)amount);
            amountConcept.Functions.Add(new Descida(level.Low, 999, 3000, 5000));
            amountConcept.Functions.Add(new Trapezoidal(level.Medium, 4000, 6000, 8000, 10000));
            amountConcept.Functions.Add(new Trapezoidal(level.High, 9500, 12500, 15500, 20000));
            amountConcept.Functions.Add(new Subida(level.VeryHigh, 17000, 25000, 37100));

            return new Dictionary<concepts, Concept>
            {
                { concepts.Risk, risk },
                { concepts.Salary, salaryConcept },
                { concepts.EmploymentTime, employmentTimeConcept },
                { concepts.Age, ageConcept },
                { concepts.Dependents, dependentsConcept },
                { concepts.Amount, amountConcept }
            };
        }
    }
}
