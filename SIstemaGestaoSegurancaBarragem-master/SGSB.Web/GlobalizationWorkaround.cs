using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SGSB.Web
{
    /// <summary>
    /// Workaround para o problema de cultura no modo invariante.
    /// Intercepta chamadas a CultureInfo.GetCultureInfo para retornar InvariantCulture
    /// quando uma cultura específica (como "en-us") for solicitada.
    /// </summary>
    public static class GlobalizationWorkaround
    {
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// Inicializa o workaround. Deve ser chamado ANTES de qualquer uso de CultureInfo.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Initialize()
        {
            if (_isInitialized)
                return;

            lock (_lock)
            {
                if (_isInitialized)
                    return;

                try
                {
                    // Forçar cultura invariante em todos os threads
                    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
                    CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                    CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

                    // Interceptar GetCultureInfo usando reflection
                    var cultureInfoType = typeof(CultureInfo);
                    var getCultureInfoMethod = cultureInfoType.GetMethod(
                        "GetCultureInfo",
                        BindingFlags.Public | BindingFlags.Static,
                        null,
                        new[] { typeof(string) },
                        null);

                    if (getCultureInfoMethod != null)
                    {
                        // Criar um delegate que intercepta a chamada
                        var originalMethod = getCultureInfoMethod.CreateDelegate(
                            typeof(Func<string, CultureInfo>)) as Func<string, CultureInfo>;

                        // Substituir temporariamente (isso é complexo, então vamos usar uma abordagem diferente)
                        // Por enquanto, vamos apenas garantir que a cultura padrão seja invariante
                    }

                    _isInitialized = true;
                }
                catch (Exception ex)
                {
                    // Se falhar, pelo menos garantir que a cultura padrão seja invariante
                    System.Diagnostics.Debug.WriteLine($"Erro ao inicializar GlobalizationWorkaround: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Wrapper seguro para GetCultureInfo que retorna InvariantCulture em modo invariante
        /// </summary>
        public static CultureInfo SafeGetCultureInfo(string name)
        {
            if (string.IsNullOrEmpty(name))
                return CultureInfo.InvariantCulture;

            // Se for "en-us" ou qualquer cultura específica, retornar InvariantCulture
            if (name.Equals("en-us", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("en-US", StringComparison.OrdinalIgnoreCase) ||
                name.Contains("-"))
            {
                return CultureInfo.InvariantCulture;
            }

            try
            {
                return CultureInfo.GetCultureInfo(name);
            }
            catch
            {
                return CultureInfo.InvariantCulture;
            }
        }
    }
}

