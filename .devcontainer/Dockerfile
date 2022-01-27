# Any Debian/Ubuntu-based image can be used.
FROM mcr.microsoft.com/vscode/devcontainers/base:bullseye

# Enable new "BUILDKIT" mode for Docker CLI.
ENV DOCKER_BUILDKIT=1

# Options for setup script
ARG INSTALL_ZSH="true"
ARG UPGRADE_PACKAGES="false"
ARG USE_MOBY="true"
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID
ARG KUBECTL_VERSION=latest
ARG HELM_VERSION=latest
ARG MINIKUBE_VERSION=latest
# Install needed packages and setup non-root user. Use a separate RUN statement to add your own dependencies.
COPY library-scripts/*.sh /tmp/library-scripts/
RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && /bin/bash /tmp/library-scripts/common-debian.sh "${INSTALL_ZSH}" "${USERNAME}" "${USER_UID}" "${USER_GID}" "${UPGRADE_PACKAGES}" "true" "true" \
    && /bin/bash /tmp/library-scripts/docker-in-docker-debian.sh "true" "${USERNAME}" "${USE_MOBY}" \
    && /bin/bash /tmp/library-scripts/kubectl-helm-debian.sh "${KUBECTL_VERSION}" "${HELM_VERSION}" "${MINIKUBE_VERSION}" \
    && mkdir -p /home/${USERNAME}/.minikube \
    && chown ${USERNAME} /home/${USERNAME}/.minikube \
    && apt-get install -y pkg-config zip g++ zlib1g-dev unzip python \
    && apt-get autoremove -y && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

# Install Bazel & Buildifier.
ARG BAZELISK_VERSION
ARG BAZELISK_DOWNLOAD_SHA=dev-mode
ARG BUILDIFIER_VERSION
ARG BUILDIFIER_DOWNLOAD_SHA=dev-mode
RUN curl -fSsL -o /usr/local/bin/bazelisk https://github.com/bazelbuild/bazelisk/releases/download/v${BAZELISK_VERSION}/bazelisk-linux-amd64 \
    && ([ "${BAZELISK_DOWNLOAD_SHA}" = "dev-mode" ] || echo "${BAZELISK_DOWNLOAD_SHA} */usr/local/bin/bazelisk" | sha256sum --check - ) \
    && chmod 0755 /usr/local/bin/bazelisk \
    && ln -s /usr/local/bin/bazelisk /usr/local/bin/bazel \
    && bazel version \
    && sudo curl -fSsL -o /usr/local/bin/buildifier https://github.com/bazelbuild/buildtools/releases/download/${BUILDIFIER_VERSION}/buildifier-linux-amd64 \
    && ([ "${BUILDIFIER_DOWNLOAD_SHA}" = "dev-mode" ] || echo "${BUILDIFIER_DOWNLOAD_SHA} */usr/local/bin/buildifier" | sha256sum - ) \
    && sudo chmod 0755 /usr/local/bin/buildifier \
    && buildifier -version

# Install Tilt & Ctlptl.
ARG TILT_VERSION
ARG TILT_DOWNLOAD_SHA=dev-mode
ARG CTLPTL_VERSION
ARG CTLPTL_DOWNLOAD_SHA=dev-mode
RUN curl -fsSL -o /usr/local/bin/tilt.tar.gz https://github.com/tilt-dev/tilt/releases/download/v${TILT_VERSION}/tilt.${TILT_VERSION}.linux.x86_64.tar.gz \
    && ([ "${TILT_DOWNLOAD_SHA}" = "dev-mode" ] || echo "${TILT_DOWNLOAD_SHA} */usr/local/bin/tilt.tar.gz" | sha256sum --check - ) \
    && tar -xzvf /usr/local/bin/tilt.tar.gz -C /usr/local/bin tilt \
    && rm -f /usr/local/bin/tilt.tar.gz \
    && chmod 0755 /usr/local/bin/tilt \
    && tilt version \
    && curl -fsSL -o /usr/local/bin/ctlptl.tar.gz https://github.com/tilt-dev/ctlptl/releases/download/v${CTLPTL_VERSION}/ctlptl.${CTLPTL_VERSION}.linux.x86_64.tar.gz \
    && ([ "${CTLPTL_DOWNLOAD_SHA}" = "dev-mode" ] || echo "${CTLPTL_DOWNLOAD_SHA} */usr/local/bin/ctlptl.tar.gz" | sha256sum --check - ) \
    && tar -xzvf /usr/local/bin/ctlptl.tar.gz -C /usr/local/bin ctlptl \
    && rm -f /usr/local/bin/ctlptl.tar.gz \
    && chmod 0755 /usr/local/bin/ctlptl \
    && ctlptl version

# [Optional] Uncomment this section to install additional OS packages.
# RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
#     && apt-get -y install --no-install-recommends <your-package-list-here>

VOLUME [ "/var/lib/docker" ]

# Setting the ENTRYPOINT to docker-init.sh will start up the Docker Engine
# inside the container ("overrideCommand": false is set in devcontainer.json).
# The script will also execute CMD if you need to alter startup behaviors.
ENTRYPOINT [ "/usr/local/share/docker-init.sh" ]
CMD [ "sleep", "infinity" ]
