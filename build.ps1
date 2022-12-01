if (Test-Path build) { Remove-Item -Recurse -Force -ErrorAction Ignore build }

# Build
python build.py
cp package.json build/package.json

# Upload build log
ossutil cp -f --acl public-read build.log ${env:OSS_DIR_BASE}/
echo ${env:OSS_URL_BASE}/build.log
echo ${env:OSS_URL_BASE_SG}/build.log

if (${env:APP_TARGET}.ToLower() -eq "Windows".ToLower()) {
    # Sign code
    mv "build/${env:APP_NAME}.exe" "build/${env:APP_NAME}-unsigned.exe"
    osslsigncode sign -spc C:\Certs\codesign.spc -key C:\Certs\codesign.key -in "build/${env:APP_NAME}-unsigned.exe" -out "build/${env:APP_NAME}.exe" -pass VRcollab1123581321 -n "${env:APP_NAME}" -i https://vrcollab.com
    Remove-Item -Force -ErrorAction Ignore "build/${env:APP_NAME}-unsigned.exe"
}

if (${env:APP_TARGET}.ToLower() -eq "WebGL".ToLower()) {
    ossutil cp -f -r --acl public-read build/ ${env:OSS_WEBAPP_BASE}/
    ossutil rm -f -r ${env:OSS_WEBAPP_LATEST}/
    ossutil cp -f -r --acl public-read build/ ${env:OSS_WEBAPP_LATEST}/
    ossutil set-meta -f -r ${env:OSS_WEBAPP_BASE}/ Content-Encoding:br --include "*.unityweb"
    ossutil set-meta -f -r ${env:OSS_WEBAPP_LATEST}/ Content-Encoding:br --include "*.unityweb"
    echo ${env:OSS_WEBAPP_URL_BASE}
    echo ${env:OSS_WEBAPP_URL_LATEST}
}

# Package artifacts
cd build ; zip -qr "${env:APP_BUNDLE_ID}.zip" * -x "*_DoNotShip/*" ; cd .. # Important to have a flatten zip structure

# Upload artifacts
ossutil cp -f --acl public-read build/package.json ${env:OSS_DIR_BASE}/
ossutil cp -f --acl public-read "build/${env:APP_BUNDLE_ID}.zip" ${env:OSS_DIR_BASE}/
echo ${env:OSS_URL_BASE}/${env:APP_BUNDLE_ID}.zip
echo ${env:OSS_URL_BASE_SG}/${env:APP_BUNDLE_ID}.zip


wecom-bot 3413a475-91c0-4b7f-897d-c4ebdc77689b "VRcollab Continuous Integration System" "Project: <font color='info'>${env:CI_PROJECT_NAME}</font>" "Build Finished!" "> Dowload Logs: [Log](${env:OSS_URL_BASE}/build.log)" "> Download Artifacts: [Artifacts](${env:OSS_URL_BASE}/${env:APP_BUNDLE_ID}.zip)" "> Web App Preview (If Exists): [Web App](${env:OSS_WEBAPP_URL_BASE})"
