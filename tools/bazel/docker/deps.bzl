"Docker rules dependencies"

load("@io_bazel_rules_docker//repositories:repositories.bzl", "repositories")
load("@io_bazel_rules_docker//repositories:deps.bzl", "deps")

def docker_dependencies():
    repositories()
    deps()
