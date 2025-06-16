using System.Text.RegularExpressions;

namespace Teste_RH.Application.Validators.Extensions;

public static class DocumentValidatorExtensions
{
    public static bool IsValidCpf(this string cpf)
    {
        cpf = Regex.Replace(cpf ?? "", @"[^\d]", "");
        if (cpf.Length != 11 || Regex.IsMatch(cpf, @"^(.)\1+$"))
            return false;

        var numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        for (int j = 9; j < 11; j++)
        {
            int sum = 0;
            for (int i = 0; i < j; i++)
                sum += numbers[i] * (j + 1 - i);

            int remainder = (sum * 10) % 11;
            if (remainder == 10) remainder = 0;
            if (numbers[j] != remainder) return false;
        }

        return true;
    }

    public static bool IsValidCnpj(this string cnpj)
    {
        cnpj = Regex.Replace(cnpj ?? "", @"[^\d]", "");
        if (cnpj.Length != 14 || Regex.IsMatch(cnpj, @"^(.)\1+$"))
            return false;

        int[] multiplicator1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicator2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj[..12];
        int sum = 0;

        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplicator1[i];

        int remainder = (sum % 11);
        int digit1 = remainder < 2 ? 0 : 11 - remainder;

        tempCnpj += digit1;
        sum = 0;

        for (int i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplicator2[i];

        remainder = (sum % 11);
        int digit2 = remainder < 2 ? 0 : 11 - remainder;

        return cnpj.EndsWith($"{digit1}{digit2}");
    }
}
