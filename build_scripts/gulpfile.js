"use strict";

// Load plugins
const gulp = require("gulp");
const _ = require("lodash");
const q = require("q");
const exec = require("child_process").exec;
const spawn = require("cross-spawn");
const util = require("util");
const fs = require("fs");
const path = require("path");
const gutil = require("gulp-util");
const gulpMSbuild = require("gulp-msbuild");
const gulpExpectFile = require("gulp-expect-file");
const del = require("del");

const msDeployCommandTemplate =
  "@if %%_echo%%!==! echo off\r\n" +
  "setlocal\r\n" +
  "@rem ---------------------------------------------------------------------------------\r\n" +
  "@rem Please Make sure you have Web Deploy install in your machine. \r\n" +
  "@rem Alternatively, you can explicit set the MsDeployPath to the location it is on your machine\r\n" +
  '@rem set MSDeployPath="C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\"\r\n' +
  "@rem ---------------------------------------------------------------------------------\r\n" +
  "                      \r\n" +
  "@rem ---------------------------------------------------------------------------------\r\n" +
  "@rem if user does not set MsDeployPath environment variable, we will try to retrieve it from registry.\r\n" +
  "@rem ---------------------------------------------------------------------------------\r\n" +
  'if "%%MSDeployPath%%" == "" (\r\n' +
  'for /F "usebackq tokens=1,2,*" %%%%h  in (`reg query "HKLM\\SOFTWARE\\Microsoft\\IIS Extensions\\MSDeploy" /s  ^| findstr -i "InstallPath"`) do (\r\n' +
  'if /I "%%%%h" == "InstallPath" ( \r\n' +
  'if /I "%%%%i" == "REG_SZ" ( \r\n' +
  'if not "%%%%j" == "" ( \r\n' +
  'if "%%%%~dpj" == "%%%%j" ( \r\n' +
  "set MSDeployPath=%%%%j\r\n" +
  "))))))\r\n" +
  "\r\n" +
  "@rem ------------------------------------------\r\n" +
  "set RootPath=%~dp0\r\n" +
  "\r\n" +
  "@rem ------------------------------------------\r\n" +
  "\r\n" +
  'if not exist "%%MSDeployPath%%msdeploy.exe" (\r\n' +
  "echo. msdeploy.exe is not found on this machine. Please install Web Deploy before execute the script. \r\n" +
  ")\r\n" +
  "\r\n" +
  "\r\n" +
  "@rem ---------------------------------------------------------------------------------\r\n" +
  "@rem Execute msdeploy.exe command line\r\n" +
  "@rem ---------------------------------------------------------------------------------\r\n" +
  "echo. Start executing msdeploy.exe\r\n" +
  "echo -------------------------------------------------------\r\n" +
  "\r\n" +
  'set _MSDeployCommandline="%%MSDeployPath%%msdeploy.exe" %s\r\n' +
  "\r\n" +
  "echo. %%_MSDeployCommandline%%\r\n" +
  "%%_MSDeployCommandline%%\r\n" +
  "goto :eof\r\n";

function getProjName(projFile) {
  return projFile.match(/^(?:.*(?:\\|\/))?(.*)\.csproj$/)[1];
}

function cmdDeployTemplate(projFile) {
  var template = "call %s.deploy.cmd /Y\r\n";
  return util.format(template, getProjName(projFile));
}

function cmdRecycleTemplate(projFile) {
  var template = "call %s.recycle.cmd\r\n";
  return util.format(template, getProjName(projFile));
}

function cmdTakeOfflineTemplate(projFile) {
  var template = "call %s.takeOffline.cmd\r\n";
  return util.format(template, getProjName(projFile));
}

function recycleTemplate(iisAppName) {
  var command = util.format(
    '-verb:sync -source:recycleApp -dest:recycleApp="%s",recycleMode="RecycleAppPool"',
    iisAppName
  );
  return util.format(msDeployCommandTemplate, command);
}

function takeOfflineTemplate(iisAppName) {
  var command = util.format(
    '-verb:sync -source:contentPath="%RootPath%app_offline.htm" -dest:contentPath=%s\\app_offline.htm',
    iisAppName
  );
  return util.format(msDeployCommandTemplate, command);
}

function getBuildDir(machine, conf, rev) {
  return util.format("./build/eumis@%s-%s/", rev, conf);
}

function command(cmd, args, cwd, commandName) {
  return function() {
    gutil.log(gutil.colors.cyan("running " + commandName));

    var deferred = q.defer(),
      child = spawn(cmd, args, { cwd: cwd, stdio: "inherit" });

    child.on("close", function(exitCode) {
      if (exitCode === 0) {
        deferred.resolve();
        return;
      }

      deferred.reject();
    });

    return deferred.promise.then(function() {
      gutil.log(gutil.colors.cyan("done " + commandName));
    });
  };
}

function getRevision() {
  var deferred = q.defer();
  exec("git rev-parse HEAD", function(error, stdout) {
    deferred.resolve(stdout.trim().substr(0, 6));
  });
  return deferred.promise;
}

function msbuild(opts, rev, conf, buildDir) {
  var buildTarget = opts.projFile || opts.slnFile;
  gutil.log(gutil.colors.cyan("building " + buildTarget));

  var deferred = q.defer(),
    version = "1.0.0.0",
    fullVersion = version + "#" + rev,
    options = {
      errorOnFail: true,
      configuration: conf,
      stdout: true,
      verbosity: "quiet",
      toolsVersion: "auto",
      properties: {
        PlatformTarget: "x64",
        AssemblyVersion: version,
        FullAssemblyVersion: fullVersion,
        TreatWarningsAsErrors: true
      }
    };

  if (opts.task === "build") {
    // jscs:disable requireCamelCaseOrUpperCaseIdentifiers
    options = _.merge(options, {
      targets: ["Build"],
      properties: {
        Disable_CopyWebApplication: true,
        OutputPath: opts.deployDir ? opts.deployDir : buildDir
      }
    });
    // jscs:enable requireCamelCaseOrUpperCaseIdentifiers
  } else if (opts.task === "package") {
    if (opts.projFile) {
      options = _.merge(options, {
        targets: ["Package"],
        properties: {
          PackageLocation: buildDir
        }
      });

      if (opts.confIisAppName && opts.confIisAppName[conf]) {
        options = _.merge(options, {
          properties: {
            DefaultDeployIisAppPath: opts.confIisAppName[conf]
          }
        });
      }
    } else if (opts.slnFile && opts.projects) {
      options = _.merge(options, {
        targets:
          // we can pass multiple <project:target> configurations for msbuild to produce at once
          _.map(opts.projects, function(p) {
            // msbuild requires project names' dots to be replaced with underscores
            return p.split(".").join("_") + ":Package";
          }),
        properties: {
          PackageLocation: buildDir
        }
      });
    }

    if (opts.parametersXMLFile && opts.parametersXMLFile[conf]) {
      options = _.merge(options, {
        properties: {
          ProjectParametersXMLFile: opts.parametersXMLFile[conf] //relative to the projfile
        }
      });
    }
  }

  gulp
    .src(buildTarget)
    .pipe(
      gulpExpectFile(
        {
          checkRealFile: true,
          errorOnFailure: true,
          silent: true
        },
        buildTarget
      )
    )
    .pipe(gulpMSbuild(options))
    .on("finish", deferred.resolve)
    .on("error", deferred.reject);

  return deferred.promise;
}

function buildProject(proj, rev, conf, buildDir) {
  var tasks = [];

  if (proj.npm) {
    tasks.push(
      command(
        "npm",
        ["run", proj.npm.confTask[conf]],
        proj.npm.location,
        "npm run " + proj.npm.confTask[conf] + " at " + proj.npm.location
      )
    );
  }
  if (proj.msbuild) {
    tasks.push(function() {
      return msbuild(proj.msbuild, rev, conf, buildDir);
    });
    if (
      proj.msbuild.recycle &&
      proj.msbuild.projFile &&
      proj.msbuild.confIisAppName &&
      proj.msbuild.confIisAppName[conf]
    ) {
      tasks.push(function() {
        var deferred = q.defer();
        fs.writeFile(
          path.join(
            buildDir,
            "./" + getProjName(proj.msbuild.projFile) + ".recycle.cmd"
          ),
          recycleTemplate(proj.msbuild.confIisAppName[conf]),
          { flag: "w+" },
          deferred.makeNodeResolver()
        );
        return deferred.promise;
      });
    }
    if (
      proj.msbuild.takeOffline &&
      proj.msbuild.projFile &&
      proj.msbuild.confIisAppName &&
      proj.msbuild.confIisAppName[conf]
    ) {
      tasks.push(function() {
        var deferred = q.defer();
        fs.writeFile(
          path.join(
            buildDir,
            "./" + getProjName(proj.msbuild.projFile) + ".takeOffline.cmd"
          ),
          takeOfflineTemplate(proj.msbuild.confIisAppName[conf]),
          { flag: "w+" },
          deferred.makeNodeResolver()
        );
        return deferred.promise;
      });
    }
  }

  return _.reduce(tasks, q.when, q(0));
}

function buildAll(projects, rev, conf, buildDir) {
  return _(projects)
    .map(function(proj) {
      return function() {
        return buildProject(proj, rev, conf, buildDir);
      };
    })
    .reduce(q.when, q(0));
}

function createPackage(projects, machine, conf) {
  return getRevision().then(function(rev) {
    var relativeBuildDir = getBuildDir(machine, conf, rev);
    var buildDir = path.join(__dirname, relativeBuildDir);
    return buildAll(projects, rev, conf, buildDir).then(function() {
      var def1 = q.defer(),
        def2 = q.defer(),
        def3 = q.defer(),
        deployAllLocation = path.join(buildDir, "./deploy_all.cmd"),
        takeOfflineAllLocation = path.join(buildDir, "./takeOffline_all.cmd"),
        deployAllContents = _(projects).reduce(function(str, proj) {
          var next = str;
          if (
            proj.msbuild &&
            proj.msbuild.projFile &&
            proj.msbuild.confIisAppName &&
            proj.msbuild.confIisAppName[conf]
          ) {
            next += cmdDeployTemplate(
              proj.msbuild.projFile,
              proj.msbuild.confIisAppName[conf]
            );

            if (proj.msbuild.recycle) {
              next += cmdRecycleTemplate(proj.msbuild.projFile);
            }
          }
          return next;
        }, ""),
        takeOfflineAllContents = _(projects).reduce(function(str, proj) {
          var next = str;
          if (
            proj.msbuild &&
            proj.msbuild.projFile &&
            proj.msbuild.confIisAppName &&
            proj.msbuild.confIisAppName[conf] &&
            proj.msbuild.takeOffline
          ) {
            next += cmdTakeOfflineTemplate(proj.msbuild.projFile);
          }
          return next;
        }, "");

      fs.writeFile(
        deployAllLocation,
        deployAllContents,
        { flag: "w+" },
        def1.makeNodeResolver()
      );

      fs.writeFile(
        takeOfflineAllLocation,
        takeOfflineAllContents,
        { flag: "w+" },
        def2.makeNodeResolver()
      );

      gulp
        .src("./app_offline.htm")
        .pipe(gulp.dest(relativeBuildDir))
        .on("finish", def3.resolve)
        .on("error", def3.reject);

      return q.all([def1, def2, def3]).then(function() {
        gutil.log(
          gutil.colors.cyan(
            "Build package succesfully created at " + deployAllLocation
          )
        );
      });
    });
  });
}

function build(done) {
  if (!gutil.env.d) {
    gutil.log(gutil.colors.red("The --deployment flag is mandatory."));
    return q.reject();
  }

  var deployment = JSON.parse(
    fs.readFileSync(path.join(__dirname, gutil.env.d + ".json"), "utf8")
  );
  var configuration = deployment.defaultConfiguration;

  if (!configuration) {
    gutil.log(
      gutil.colors.red(
        "The deployment has no defaultConfiguration so the --configuration flag is mandatory."
      )
    );
    return q.reject();
  }

 if(deployment.machines[gutil.env.host]){
    createPackage(deployment.machines[gutil.env.host], gutil.env.host, configuration);
  }
  else{
    console.log(typeof gutil.env.host);  
    if(typeof gutil.env.host === 'string'){
      gutil.log(
        `Could not find host ${gutil.colors.red(gutil.env.host)} in settings file. Proceed with build for all hosts`
      );
    }
    _.toPairs(deployment.machines)
    .map(function([machine, projects]) {
      return function() {
        return createPackage(projects, machine, configuration);
      };
    })
    .reduce(q.when, q(0));
  }

  done();
}

// Clean assets
function clean() {
  return del(["./build/"]);
}

// Export tasks
exports.build = build;
exports.clean = clean;
exports.package = gulp.series(clean, build);
