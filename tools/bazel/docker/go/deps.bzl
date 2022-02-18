"Go containers dependencies"

load("@io_bazel_rules_docker//go:image.bzl", "repositories")
load("@io_bazel_rules_docker//container:container.bzl", "container_pull")

def docker_go_dependencies():
    repositories()
    container_pull(
        name = "go_base",
        registry = "docker.io",
        repository = "library/golang",
        # The 'digest' field should be used instead of 'tag' to achieve
        # canonical reproducibility.
        digest = "sha256:f28579af8a31c28fc180fb2e26c415bf6211a21fb9f3ed5e81bcdbf062c52893",
        # tag = "alpine",
    )
