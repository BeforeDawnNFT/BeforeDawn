#!/usr/bin/python3

import argparse
import os
import subprocess
import time

PROJECT_DIR = os.path.dirname(os.path.realpath(__file__))


def getarg(args, name, default=None):
    val = default
    if os.getenv(name):
        val = os.getenv(name)
    if args[name]:
        val = args[name]
    if not val:
        print("Error: " + name + " is not defined")
        exit(1)
    return val


def exit_if_err(code):
    if code != 0:
        os.system(f"cat {PROJECT_DIR}/build.log")
        exit(1)


def build_unity(args):
    for arg in args:
        if args[arg]:
            os.environ[arg] = str(args[arg])
    target = args["APP_TARGET"]
    if target is None or target == "":
        target = "Windows"
    if target == "Windows":
        target = "Win64"
    cmd = [os.getenv("UNITY"), "-quit", "-batchmode", "-projectPath",
           PROJECT_DIR, "-logfile", "-", "-buildTarget", target, "-executeMethod", "BuildScript.Build"]
    now = time.time()
    code = os.system(f"{' '.join(cmd)} > {PROJECT_DIR}/build.log 2>&1")
    exit_if_err(code)
    seconds = time.time() - now
    print("Build Time (s): " + str(seconds))


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Build script for Unity")
    parser.add_argument("--name",       dest="APP_NAME")
    parser.add_argument("--bundleId",   dest="APP_BUNDLE_ID")
    parser.add_argument("--target",     dest="APP_TARGET")
    parser.add_argument("--version",    dest="APP_VERSION")
    parser.add_argument('--deploy',     dest="APP_DEPLOY",
                        action=argparse.BooleanOptionalAction)
    args = parser.parse_args()
    args = vars(args)
    args["APP_NAME"]      = getarg(args, "APP_NAME")
    args["APP_BUNDLE_ID"] = getarg(args, "APP_BUNDLE_ID")
    args["APP_TARGET"]    = getarg(args, "APP_TARGET")
    args["APP_VERSION"]   = getarg(args, "APP_VERSION")
    build_unity(args)
