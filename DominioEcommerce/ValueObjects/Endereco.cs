using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.ValueObjects
{
    public record Endereco
    {
        public string Logradouro { get; init; } = string.Empty;
        public string Numero { get; init; } = string.Empty;
        public string Complemento { get; init; } = string.Empty;
        public string Bairro { get; init; } = string.Empty;
        public string Cidade { get; init; } = string.Empty;
        public string Estado { get; init; } = string.Empty;
        public string Cep { get; init; } = string.Empty;

        public Endereco(string cep, string logradouro, string numero, string bairro, string cidade, string estado, string? complemento = null)
        {
            Cep = cep?.Trim() ?? string.Empty;
            Logradouro = logradouro?.Trim() ?? string.Empty;
            Numero = numero?.Trim() ?? string.Empty;
            Bairro = bairro?.Trim() ?? string.Empty;
            Cidade = cidade?.Trim() ?? string.Empty;
            Estado = estado?.Trim().ToUpper() ?? string.Empty;
            Complemento = complemento?.Trim();


            Validar();
        }

        private void Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Cep)) erros.Add("O CEP é obrigatório.");
            if (string.IsNullOrWhiteSpace(Logradouro)) erros.Add("O logradouro é obrigatório.");
            if (string.IsNullOrWhiteSpace(Numero)) erros.Add("O número é obrigatório. Use 'S/N' se não houver.");
            if (string.IsNullOrWhiteSpace(Bairro)) erros.Add("O bairro é obrigatório.");
            if (string.IsNullOrWhiteSpace(Cidade)) erros.Add("A cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(Estado) || Estado.Length != 2) erros.Add("O estado é obrigatório e deve ter 2 caracteres (Ex: RJ).");

            if (erros.Any())
            {
                throw new DominioException.DominioException("Inconsistências no endereço de entrega.", erros);
            }
        }
    }
}
