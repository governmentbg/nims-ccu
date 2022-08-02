/*eslint-env node*/
class WebpackDonePlugin {
  apply(compiler) {
    compiler.hooks.done.tap('Done building', stats => {
      const time = new Date(stats.startTime).toLocaleTimeString('en-US', { hour12: false });
      // eslint-disable-next-line no-console
      console.log(
        // prettier-ignore
        stats.hasErrors() ? '\x1b[31m' : // red color for errors
          stats.hasWarnings() ? '\x1b[33m' : // yellow for warnings
          '\x1b[32m', // green for none
        `Build started at ${time} and finished for ${stats.endTime - stats.startTime} ms`,
        '\x1b[0m' // reset console color
      );
    });
  }
}

module.exports = WebpackDonePlugin;
