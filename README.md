# Hololens2 Accuracy Tool
Tool for Hololens2 to measure handtracking accuracy, made with Unity 2020.3.30f1. 2022 Internship, LIVE-ÉTS Montréal


• This project conducted at ÉTS Montréal is about evaluating the accuracy of the augmented reality helmet “HoloLens2” from Microsoft in terms of hand position detection, for a potential use of augmented reality and HoloLens2 in the medical field and in cardiology.
A Unity application was developed to record and save the coordinates measured by the HoloLens2 in real time on the thumb and index. The device used as a reference is a sensor / transmitter of electromagnetic waves from NDI, used in the medical field and with a precision above one millimetre. To relate the data from both devices, a QR code detection system along with a coordinate system creator was also developed.
Experiments simulating various types of gestures of the hand have been made, and the first results exploited, showing that HoloLens2’s accuracy is rather low and potentially insufficient for a medical use.

• Some tests were made to see if moving object detection was accurate enough to follow a medical tool during an intervention, using VisionLib's library, but it appreared that on small objects the detection was not stable nor accurate enough to pursue.
This project is available on the "Object Tracking" Branch, but is a different Unity project from the precision tool.

• What I used :

- **Mixed Reality Toolkit 2 :** https://docs.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk2/?view=mrtkunity-2022-05
- **OpenXR :** https://docs.microsoft.com/en-us/windows/mixed-reality/develop/native/openxr
- **Microsoft QR Tracking :** https://github.com/microsoft/MixedReality-QRCode-Sample
- **This project of QR object positionning :** https://github.com/LocalJoost/QRCodeService/tree/openxr

- **VisionLib :** https://visionlib.com/
