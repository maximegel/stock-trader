"Kubernetes rules dependencies"

load("@io_bazel_rules_k8s//k8s:k8s.bzl", "k8s_repositories")
load("@io_bazel_rules_k8s//k8s:k8s_go_deps.bzl", "deps")

def k8s_dependencies():
    k8s_repositories()
    deps()
