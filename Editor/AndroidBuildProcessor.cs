using System.IO;
using UnityEditor.Android;
using UnityEngine;

public class AndroidBuildProcessor : IPostGenerateGradleAndroidProject
{
    private AdServerSettings _settings;

    public int callbackOrder => 999;

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        _settings = AdServerSettingsEditor.GetSettings();

        if (_settings.AutoUpdateManifest)
        {
            string manifestPath = Path.Combine(path, "src/main/AndroidManifest.xml");
            UpdateAndroidManifest(manifestPath, _settings.AdServerKey, _settings.AdServerGoogleKey);
        }

        if (_settings.AutoUpdateBuildGradle)
        {
            string buildGradlePath = Path.Combine(path, "build.gradle");
            UpdateBuildGradle(buildGradlePath, _settings.AdSdkVersion);
        }

        if (_settings.AutoUpdateSettingsGradle)
        {
            string settingsGradlePath = Path.Combine(path, "..", "settings.gradle");
            UpdateSettingsGradle(settingsGradlePath);
        }

        if (_settings.AutoUpdateGradleProperties)
        {
            string gradlePropertiesPath = Path.Combine(path, "..", "gradle.properties");
            UpdateGradleProperties(gradlePropertiesPath);
        }
    }

    private void UpdateAndroidManifest(string manifestPath, string adServerKey, string adServerGoogleKey)
    {
        string manifestContents = File.ReadAllText(manifestPath);
        if (!manifestContents.Contains("com.adgrowth.adserver.CLIENT_KEY"))
        {
            string metaDataTag = $"<meta-data android:name=\"com.adgrowth.adserver.CLIENT_KEY\" android:value=\"{_settings.AdServerKey}\" />";
            manifestContents = manifestContents.Replace("</application>", $"{metaDataTag}\n</application>");
        }

        if (!manifestContents.Contains("com.google.android.gms.ads.APPLICATION_ID"))
        {
            string metaDataTag = $"<meta-data android:name=\"com.google.android.gms.ads.APPLICATION_ID\" android:value=\"{_settings.AdServerGoogleKey}\" />";
            manifestContents = manifestContents.Replace("</application>", $"{metaDataTag}\n</application>");
        }


        string clientKeyPattern = "android:name=\"com.adgrowth.adserver.CLIENT_KEY\" android:value=\"(.+?)\"";
        string applicationIdPattern = "android:name=\"com.google.android.gms.ads.APPLICATION_ID\" android:value=\"(.+?)\"";

        manifestContents = UpdateManifestValue(manifestContents, clientKeyPattern, adServerKey);
        manifestContents = UpdateManifestValue(manifestContents, applicationIdPattern, adServerGoogleKey);

        File.WriteAllText(manifestPath, manifestContents);
    }

    private string UpdateManifestValue(string manifestContents, string pattern, string newValue)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
        var match = regex.Match(manifestContents);

        if (match.Success)
        {
            string oldValue = match.Groups[1].Value;
            manifestContents = manifestContents.Replace(oldValue, newValue);
        }

        return manifestContents;
    }

    private void UpdateBuildGradle(string buildGradlePath, string adSdkVersion)
    {
        string buildGradleContents = File.ReadAllText(buildGradlePath);

        if (!buildGradleContents.Contains("com.github.Ad-Growth:ad-sdk-android"))
        {
            string dependency = $"implementation 'com.github.Ad-Growth:ad-sdk-android:{_settings.AdSdkVersion}'";
            buildGradleContents = buildGradleContents.Replace("dependencies {", $"dependencies {{\n    {dependency}\n");
            File.WriteAllText(buildGradlePath, buildGradleContents);

            return;
        }

        string oldDependencyPattern = "implementation 'com.github.Ad-Growth:ad-sdk-android:(.+)'";
        string newDependency = $"implementation 'com.github.Ad-Growth:ad-sdk-android:{adSdkVersion}'";

        buildGradleContents = System.Text.RegularExpressions.Regex.Replace(buildGradleContents, oldDependencyPattern, newDependency);

        File.WriteAllText(buildGradlePath, buildGradleContents);
    }

    private void UpdateSettingsGradle(string settingsGradlePath)
    {
        string repository = "maven { url 'https://jitpack.io' }";
        string settingsGradleContents = File.ReadAllText(settingsGradlePath);

        if (settingsGradleContents.Contains(repository)) return;

        string dependencyResolutionManagementBlock = "dependencyResolutionManagement";
        string repositoriesBlock = "repositories";

        int startIndex = settingsGradleContents.IndexOf(dependencyResolutionManagementBlock);

        if (startIndex == -1) return;

        int repositoriesIndex = settingsGradleContents.IndexOf(repositoriesBlock, startIndex);
        if (repositoriesIndex == -1) return;

        int insertIndex = settingsGradleContents.IndexOf("{", repositoriesIndex);
        if (insertIndex == -1) return;

        insertIndex = settingsGradleContents.IndexOf("\n", insertIndex) + 1;

        settingsGradleContents = settingsGradleContents.Insert(insertIndex, $"        {repository}\n");
        File.WriteAllText(settingsGradlePath, settingsGradleContents);
    }

    private void UpdateGradleProperties(string gradlePropertiesPath)
    {
        string androidxProperty = "android.useAndroidX=true";
        string gradlePropertiesContents = File.ReadAllText(gradlePropertiesPath);

        if (!gradlePropertiesContents.Contains(androidxProperty))
        {
            gradlePropertiesContents += $"\n\n{androidxProperty}";
            File.WriteAllText(gradlePropertiesPath, gradlePropertiesContents);
        }
    }
}
