using System;
using System.Linq;
using Microsoft.Win32;

public class GetDotNetVersion {

    public static void Main() {
        Get45PlusFromRegistry();
    }

    private static void Get45PlusFromRegistry() {

        const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
        versionMap = versionMap.OrderByDescending(v => v.releaseKey).ToArray();

        using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey)) {
            var message = ndpKey?.GetValue("Release") is int releaseKey ? ".NET Framework Version: " + CheckFor45PlusVersion(releaseKey)
                : ".NET Framework Version 4.5 or later is not detected.";
            Console.WriteLine(message);
        }
    }

    private static (int releaseKey, string version)[] versionMap = {
        (461308, "4.7.1"), (460798, "4.7"), (394802, "4.6.2"), (394254, "4.6.1"),
        (393295, "4.6"), (379893, "4.5.2"), (378675, "4.5.1"), (378389, "4.5"), (0, null)
    };

    // Checking the version using >= enables forward compatibility.
    private static string CheckFor45PlusVersion(int releaseKey) =>
        versionMap.FirstOrDefault(vMap => releaseKey >= vMap.releaseKey).version ?? "No 4.5 or later version detected";
}
