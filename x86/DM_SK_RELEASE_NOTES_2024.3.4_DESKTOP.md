# Release Notes for Dragon Medical SpeechKit 2024\.3\.4 (.NET edition 6\.3 R4\)

## Solved in version 2024\.3\.4 (6\.3 R4\)

* This release improves the security and stability of the SDK.
* Deferred correction: The playback button no longer stays grayed out in correction mode. \[1624428]

## New in version 2024\.3\.4 (6\.3 R4\)

* Sample apps delivered with the SDK now require you to specify the recognition service URL. For example, for the United States region it's:

`private const string _serviceUrl = "https://sas.nuancehdp.com/";`

No changes are required for existing integrations.

## Known issues

* When working with a Philips SpeechMike or Nuance PowerMic 4 device, the screen saver might not be displayed after the configured period of inactivity when recording is in standby mode. To disable this behavior, press\-and\-hold the left mouse and touchpad buttons on the recording device for five seconds. When the firmware setting has been changed successfully, the device provides visual and audio feedback. This setting is stored on the device and needs to be changed individually on each device. \[CAVE\-6152]
* Connecting two different control devices at the same time isn't supported. Button presses might not map to the expected actions. \[CAVE\-6093]
* When the app doesn't have permission to access the microphone, Dragon Medical SpeechKit displays an "internal audio" error. \[DNB\-44364]
* Some Citrix environments exhibit issues with the Microsoft Edge WebView2 runtime, leading the personalization and help window to display incorrectly and sometimes terminate unexpectedly. **Workaround:** Disable the Citrix Special Folder Redirection feature (SfrHook) for the Microsoft Edge WebView2 runtime process. To do this, add the following registry keys to your Citrix environment configuration:

`[HKEY_LOCAL_MACHINE\SOFTWARE\Citrix\CtxHook\AppInit_DLLs\SfrHook\msedgewebview2.exe]`

`[HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Citrix\CtxHook\AppInit_DLLs\SfrHook\msedgewebview2.exe]`

For more information on these registry keys, see the Citrix Knowledge Center: [How to Disable Citrix API Hooks on a Per\-application Basis](https://support.citrix.com/article/CTX107825)

## Important information

* Dragon Medical SpeechKit (.NET edition) requires the following .NET binding redirection in the app config file:

```xml
<dependentAssembly>
    <assemblyIdentity name="System.Runtime.CompilerServices.Unsafe" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-6.0.0.0" newVersion="6.0.0.0"/>
</dependentAssembly>
```

* We have enhanced the security of Dragon Medical SpeechKit (.NET edition) to prevent network sniffers such as Fiddler from intercepting traffic from apps that use the SDK. This change ensures that your data is more secure and less susceptible to unauthorized access or interception.
* Starting with Citrix Virtual Apps and Desktops 2109, the 'Virtual channel allow list' policy setting is enabled by default. This means that custom/third\-party virtual channels no longer work with the default configuration.

The following options are available:

Disable the 'Virtual channel allow list' policy setting to allow all virtual channels again.

Add the Nuance virtual channels to the allow list; for more information, see the [Citrix documentation](https://docs.citrix.com/en-us/citrix-virtual-apps-desktops/policies/reference/ica-policy-settings/virtual-channel-allow-list-policy-settings.html).

For more information, see the *Nuance Citrix Extension Deployment and Configuration Guide* delivered with the Nuance virtual extension package or contact Nuance Technical Support.

* Be aware that your application must be recompiled following this upgrade.
* In partner integrations based on Dragon Medical SpeechKit (.NET edition), new users without a Nuance PowerMic Mobile license who select PowerMic Mobile from the Current Device menu are notified that they must apply for a license or select another input device. \[DNB\-17840]
* To display your application's end user online help in the personalization \& help window, make sure it can be loaded in an iframe. \[DNB\-20538]
* The VUI form focus is no longer set to the speech\-enabled field when `Initialize()`, `Open()` or `Synchronize()` is called on the VuiController. \[DNB\-12643]
* The set of redistributable files was changed in version 3\.4\. Make sure to verify that all files in the **bin** folder are included with your application distribution.
* Dragon Medical SpeechKit 2024\.3 (.NET edition) is compatible with Dragon Medical Server 2023 and 2024\.
* Dragon Medical Server 2024\.3 is compatible with Dragon Medical SpeechKit and Dragon Medical Embedded versions 2023 and 2024\.
* Microsoft .NET Framework 4\.7\.2\-4\.8 must be installed on all workstations running .NET edition\-based applications.
* Microsoft Edge Webview 2 Runtime must be installed on all workstations running .NET edition\-based applications.

### Important information for deferred correction

Recognition results in the system are used to adapt authors' speech recognition profiles to improve recognition accuracy over time. An author's profile is adapted only from their own updates to a document:

* The author's initial dictations.
* The author's own corrections in frontend mode.
* Transcriptionist corrections that the author has reviewed by opening and saving the document after the transcriptionist corrected it.

Edits made by transcriptionists aren't used to adapt the author's profile unless the author opens and saves the document after the transcriptionist corrects it.

In some cases, uncorrected text can lead to reduced recognition accuracy over time. "Uncorrected text" refers to text that the system recognized incorrectly and that wasn't corrected. To mitigate this, include a final sign\-off in the deferred correction workflow and track the document state. For more information, see the *Dragon Medical SpeechKit SDK Help*.

## Infrastructure

### Infrastructure no longer supported

* Dragon Medical SpeechKit and Dragon Medical Embedded 2024\.3 applications don't work with version 2022\.2 servers.
* Version 2022\.2 client applications don't work with version 2024\.3 servers.

### Infrastructure changes planned for the next release

* Dragon Medical SpeechKit and Dragon Medical Embedded 2024\.4 and higher won't work with version 2022 servers.
* Version 2022 client applications won't work with version 2024\.4 servers.

## System requirements \- all clients\*

| Component | Recommended | Supported |
|-|-|-|
| Operating system | Windows 11 | Windows 10 Windows 11 |
| .NET Framework | Microsoft .NET Framework 4\.7\.2 | Microsoft .NET Framework 4\.7\.2\-4\.8 |
| Web browser | Microsoft Edge WebView2 runtime | Microsoft Edge WebView2 runtime |

## System requirements \- virtualized environments\*

| Component | Recommended | Supported |
|-|-|-|
| Nuance virtual extensions: Client | 124\.4\.29\.0 | 122\.4\.102\.0 \- 124\.4\.29\.0 |
| VMware Horizon View |  | VMware Horizon View 7: VMware Horizon View Agent 7\.13 or higher VMware Horizon Client 5\.5 or higher<br><br>Protocols: PCoIP, Blast Extreme, Real\-Time Audio\-Video (RTAV) |
| Citrix Virtual Apps and Desktops |  | Citrix XenApp and XenDesktop: 7\.15 or higher<br><br>Citrix Virtual Apps and Desktops: 1912 LTSR or higher |
| Citrix Client |  | Citrix Workspace app: 1912 or higher |
| Remote Desktop Services | Windows Server 2019 | Windows Server 2016 Windows Server 2019 Windows Server 2022 |
| Remote Desktop Client | Windows 11 | Windows 10 Windows 11 |
| Linux thin clients |  | IGEL (For information on the IGEL thin clients supported, search for Nuance compatibility in the [IGEL Knowledge Base](https://kb.igel.com/).) |

### Supported microphones

Audio works for all microphones listed. Support for button controls is listed below:

| Component | Citrix XenApp | Citrix XenDesktop | RDS | VMware Horizon View |
| --- | --- | --- | --- | --- |
| Nuance PowerMic 4 | yes\* | yes\* | yes\* | yes\* |
| Nuance PowerMic II | yes\* | yes\* | yes\* | yes\* |
| Nuance PowerMic II with barcode scanner | yes\* | yes\* | yes\* | yes\* |
| Nuance PowerMic III | yes\* | yes\* | yes\* | yes\* |
| Philips SpeechMike Air | yes\*\* | yes\*\* | yes\*\* | yes\*\* |
| Philips SpeechMike Premium | yes\*\* | yes\*\* | yes\*\* | yes\*\* |
| Philips SpeechMike Premium Touch | yes\*\* | yes\*\* | yes\*\* | yes\*\* |
| Philips SpeechMike Premium Air | yes\*\* | yes\*\* | yes\*\* | yes\*\* |
| Philips SpeechMike III | yes\*\* | yes\*\* | yes\*\* | yes\*\* |
| Philips SpeechOne PSM6000 headset | yes\*\* | yes\*\* | yes\*\* | yes\*\* |
| Grundig Digta SonicMic II | yes\*\* | yes\*\* | yes\*\* | yes\*\*\* |
| Grundig Digta SonicMic II (US edition) | yes\*\* | yes\*\* | yes\*\* | yes\*\*\* |
| Grundig Digta SonicMic 3 | yes\*\* | yes\*\* | yes\*\* | yes\*\*\* |

\* To enable Nuance PowerMic controls, install the Nuance PowerMic Citrix Extension/PowerMic RDS Extension/PowerMic VMware Client Extension.

\*\* To enable button controls for third\-party devices, install the corresponding redistributable packages.

\*\*\* To enable button controls for these devices, configure USB device splitting.

### Remarks

* For more information on virtualized environments, see the **Docs \& Guidelines** folder of the Nuance virtual extensions package on the [Nuance Healthcare Development Platform](http://www.nuancehealthcaredeveloper.com/).
* For security reasons, make sure that VDI channel encryption is enabled between client end points and VDI servers or virtual desktops. Disabling encryption in a virtualized environment can lead to PHI being exposed. Encryption is enabled by default.
* Nuance Citrix/RDS/VMware Audio Extension and Nuance PowerMic Citrix/RDS/VMware Extension:

The extensions do not need to be installed on the server/virtual desktop; the required server binaries are already included in the application folder.

Make sure you uninstall the corresponding server/virtual desktop component setup if it is no longer used by other products.

* Nuance RDS Audio Extension and Nuance PowerMic RDS Client Extension: UDP transport must be disabled on Remote Desktop Protocol (RDP) clients.

To disable UDP transport, add the following registry keys and value to the Remote Desktop Client, then restart the PC:

Keys:

HKEY\_LOCAL\_MACHINE\\Software\\Microsoft\\Terminal Server Client

HKEY\_LOCAL\_MACHINE\\Software\\Wow6432Node\\Microsoft\\Terminal Server Client

DWORD Value:

`DisableUDPTransport` \= `1`

* VMware Horizon View 7\.10 and higher: RTAV with device splitting is recommended if Nuance audio channels can't be used.
* To enable microphone button support for Philips and Grundig devices, contact your device vendor for drivers that support your virtualized environment; deploy them on the server/virtual desktop where your application is hosted and on the client PC.
* Microsoft RDS: When the Grundig drivers aren't installed on both RDS server and client endpoint, the application using a Grundig device might become unresponsive when reconnecting to a session.
* Philips SpeechMike on Linux thin clients in a Citrix environment: Session reconnect might not work. **Solution:** Make sure SpeechMike firmware version 2\.54 or higher is installed.
* Some Citrix environments exhibit issues with the Microsoft Edge WebView2 runtime, leading to the personalization and help window to display incorrectly and sometimes terminate unexpectedly. **Workaround:** Disable the Citrix Special Folder Redirection feature (SfrHook) for the Microsoft Edge WebView2 runtime process. To do this, add the following registry keys to your Citrix environment configuration:

`[HKEY_LOCAL_MACHINE\SOFTWARE\Citrix\CtxHook\AppInit_DLLs\SfrHook\msedgewebview2.exe]`

`[HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Citrix\CtxHook\AppInit_DLLs\SfrHook\msedgewebview2.exe]`

For more information on these registry keys, see the Citrix Knowledge Center: [How to Disable Citrix API Hooks on a Per\-application Basis](https://support.citrix.com/article/CTX107825)

## System requirements \- edit controls\*

| Component | Recommended | Supported |
|-|-|-|
| System.Windows.Forms.TextBoxBase  System.Windows.Forms.TextBox  System.Windows.Forms.MaskedTextBox System.Windows.Forms.RichTextBox | 4\.5 | 4\.5 |
| System.Windows.Controls.TextBox | 4\.5 | 4\.5 |

### \*End\-of\-life: third\-party environments

Support for third\-party environments is only valid as long as they are supported by the corresponding vendor and might be subject to other restrictions. Please contact Nuance Technical Support for details. For more information, see the documentation delivered with the third\-party product and supporting Nuance documentation.

When standard support by the vendor has stopped, Nuance will continue support if an issue is specific to the Nuance solution, within the limitations of the vendor’s standard end\-of\-life and Nuance's policy. This means that issues that are a combination of the vendor's component and the Nuance solution can't be supported.

## Dragon Medical SpeechKit product life cycle

To take advantage of technical innovations and security enhancements, it's important to stay up to date with the latest versions of our SDKs.

We use the following versioning standard:

* Major release \- represented by an increment in the major application version number; for example 2023\.1, 2023\.2\.
* Point release \- a service release between major releases, represented by an increment in the minor application version number; for example 2023\.2\.1, 2023\.2\.2\.

We provide support for the current Dragon Medical SpeechKit version and one previous version. We investigate issues in the most current version and the previous version, or all versions released within the last 12 months (whichever includes more versions). Updates that address issues identified in previous versions are always based on the most current version.

When we release a new version of Dragon Medical SpeechKit that includes breaking changes, you might need to update your application’s source code. In these cases, we continue support for the previous version, prior to the breaking changes, for 12 months.

We won't investigate issues after the end of support.

| Version | Status | End of support | End of life | Deactivation |
| --- | --- | --- | --- | --- |
| Dragon Medical SpeechKit 2020\.1 (.NET edition 3\.7 R1\) | End of life | 2021\-03\-31 | 2021\-06\-30 | 2024\-12\-30 |
| Dragon Medical SpeechKit 2020\.3 (.NET edition 3\.8 R1\) | End of life | 2021\-08\-31 | 2021\-11\-31 | 2024\-12\-30 |
| Dragon Medical SpeechKit 2020\.4 (.NET edition 3\.9 R1\) | End of life | 2021\-12\-31 | 2022\-03\-31 | 2024\-12\-30 |
| Dragon Medical SpeechKit 2021\.2 (.NET edition 3\.10 R1\) | End of life | 2022\-05\-31 | 2022\-08\-31 | 2025\-03 |
| Dragon Medical SpeechKit 2021\.3 (.NET edition 3\.11 R1\) | End of life | 2022\-09\-30 | 2022\-12\-31 | 2025\-03 |
| Dragon Medical SpeechKit 2021\.4 (.NET edition 4\.0 R1\) | End of life | 2022\-12\-31 | 2023\-03\-31 | 2025\-12 |
| Dragon Medical SpeechKit 2022\.1 (.NET edition 4\.1 R1\) | End of life | 2023\-03\-31 | 2023\-06\-31 | 2025\-12 |
| Dragon Medical SpeechKit 2022\.3 (.NET edition 4\.2 R3\) | End of life | 2023\-08\-31 | 2023\-11\-30 | 2025\-12 |
| Dragon Medical SpeechKit 2023\.2 (.NET edition 5\.0 R2\) | End of life | 2024\-05\-15 | 2024\-08\-15 |  |
| Dragon Medical SpeechKit 2024\.1 (.NET edition 6\.0 R1\) | Recalled: not supported | | | |
| Dragon Medical SpeechKit 2024\.2 (.NET edition 6\.2 R1\) | Supported | 2025\-05\-15 | 2025\-08\-15 |  |
| Dragon Medical SpeechKit 2024\.3\.4 (.NET edition 6\.3 R4\) | Supported | 2025\-09\-30\* | 2025\-12\-31\* |  |

\*Preliminary date depending on the next major or point release. If this is longer than 12 months, the support period will be extended until the next major or point release.

At Nuance, we place a strong emphasis on security and take a proactive approach to safeguarding our platform. To this end, 12 months after the end of support, Nuance deactivates unsupported client versions on the Nuance cloud. To avoid any disruption in service, we strongly recommend that partners integrating client SDKs that are in restricted maintenance mode upgrade their customers to the latest version of Dragon Medical SpeechKit as soon as possible.
