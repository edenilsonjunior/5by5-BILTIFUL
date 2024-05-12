namespace BILTIFUL.Modulo4.Utils
{
    internal class Extra
    {
        public Extra()
        {

        }
        /// <summary>
        /// Método valida se input é float.
        /// </summary>
        public static float retornarFloat()
        {
            float Quantidade = 0;
            bool valor = false;

            while (!valor)
            {
                if (float.TryParse(Console.ReadLine(), out float qtde))
                {
                    Quantidade = qtde;
                    valor = true;
                }
                else
                {
                    Console.WriteLine("Formato inválido. Preencha por exemplo 0,01 1,00 10,00 100,00");
                }
            }
            return Quantidade;
        }
        /// <summary>
        /// Método valida se input é inteiro.
        /// </summary>
        public static int retornarInt()
        {
            int Inteiro = 0;
            bool valor = false;

            while (!valor)
            {
                if (int.TryParse(Console.ReadLine(), out int varint))
                {
                    Inteiro = varint;
                    valor = true;
                }
                else
                {
                    Console.WriteLine("Formato inválido. Informe números inteiros apenas.");
                }
            }
            return Inteiro;
        }
    }
}
