cd C:\MMSPrototype
"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\TF.exe" get $/MMSPrototype /recursive
cd C:\MMSDocumentation
"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\TF.exe" get $/MMSDocumentation /recursive
ss Get "$/D21" -GL"C:\D21" -R -I
ss Get "$/D21 Wise Install" -GL"C:\D21 Wise Install" -R -I
ss Get "$/D21Supplemental" -GL"C:\D21Supplemental" -R -I