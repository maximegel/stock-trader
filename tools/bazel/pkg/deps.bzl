"Bazel package building rules dependencies"

load("@io_bazel_rules_pkg//:deps.bzl", "rules_pkg_dependencies")

def pkg_dependencies():
    rules_pkg_dependencies()
