const fs = require('fs');
const path = require('path');
const util = require('util');
const git = require('simple-git/promise');
const isValidUTF8 = require('utf-8-validate');

const readFile = util.promisify(fs.readFile);

const emptyTreeHash = '4b825dc642cb6eb9a060e54bf8d69288fbee4904'; // git hash-object -t tree /dev/null

async function isTextFile(repo, commit, path) {	
	const numstatOutput = await repo.diff(['--numstat', emptyTreeHash, commit, '--', path]);
	
	return numstatOutput && !/-\s+-/.test(numstatOutput.trim());
}

async function main () {
	if (!process.argv[2]) {
		throw new Error('Missing argument: working dir');
	}
	
	const workingDir = process.argv[2].trim();
	const startCommit = (process.argv[3] || emptyTreeHash).trim();
	const endCommit = (process.argv[4] || 'HEAD').trim();
	
	const repo = git(workingDir);
	
	const diffFilesOutput = await repo.diff([startCommit, endCommit, '--name-only']);
	const changedFiles = diffFilesOutput.split(/\r?\n/).filter(s => s);
	
	const nonUtf8TextFiles = [];
	for (let file of changedFiles) {
		let isText = await isTextFile(repo, endCommit, file);
		if (!isText) {
			continue;
		}
		
		const buf = await readFile(path.join(workingDir, file));
		
		if (!isValidUTF8(buf)) {
			nonUtf8TextFiles.push(file);
		}
	}
	
	if (nonUtf8TextFiles.length > 0) {
		throw new Error(`Non UTF8 files found:\r\n${nonUtf8TextFiles.join('\r\n')}`);
	}
}

main().catch(e => {
	console.error(e);
	process.exit(1);
});
