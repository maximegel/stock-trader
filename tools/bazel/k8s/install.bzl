"Bazel functions for installing Kubernetes rules."

load("@bazel_tools//tools/build_defs/repo:http.bzl", "http_archive")

def k8s_rules_install():
    http_archive(
        name = "io_bazel_rules_k8s",
        sha256 = "773aa45f2421a66c8aa651b8cecb8ea51db91799a405bd7b913d77052ac7261a",
        strip_prefix = "rules_k8s-0.5",
        urls = ["https://github.com/bazelbuild/rules_k8s/archive/v0.5.tar.gz"],
    )
