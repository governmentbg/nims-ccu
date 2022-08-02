& jscodeshift `
    -t .\transformers\remove-wrapper-function.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\use-angular-module-exports.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\use-angular-module-imports.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\export-angular-module-registrations.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\import-angular-module-registrations.js `
    --ignore-pattern ..\..\src\Eumis.Web.App\js\scaffolding\scaffolding_bg.js `
    --ignore-pattern ..\..\src\Eumis.Web.App\test\app\testapp_bg.js `
    --ignore-pattern ..\..\src\Eumis.Web.App\js\main\main_bg.js `
    ..\..\src\Eumis.Web.App\js\scaffolding\ `
    ..\..\src\Eumis.Web.App\test\app\ `
    ..\..\src\Eumis.Web.App\js\main
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\import-html-files.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\remove-unused-imports.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\sort-imports.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
Read-Host "Press ENTER to continue"

& jscodeshift `
    -t .\transformers\add-imports.js `
    ..\..\src\Eumis.Web.App\js\ `
    ..\..\src\Eumis.Web.App\test\app\
