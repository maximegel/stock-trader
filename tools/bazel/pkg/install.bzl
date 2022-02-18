"Bazel functions for installing Bazel package building rules."

load("@bazel_tools//tools/build_defs/repo:http.bzl", "http_archive")

def pkg_rules_install():
    http_archive(
        name = "io_bazel_rules_pkg",
        sha256 = "62eeb544ff1ef41d786e329e1536c1d541bb9bcad27ae984d57f18f314018e66",
        urls = [
            "https://mirror.bazel.build/github.com/bazelbuild/rules_pkg/releases/download/0.6.0/rules_pkg-0.6.0.tar.gz",
            "https://github.com/bazelbuild/rules_pkg/releases/download/0.6.0/rules_pkg-0.6.0.tar.gz",
        ],
    )
