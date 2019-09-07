// Copyright (c) MPGP. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using Prometheus;

namespace Mpgp.Shared
{
    public class Metrics
    {
        public static readonly Gauge UsersOnline = Prometheus.Metrics
            .CreateGauge("users_online", "Number of users online.");
    }
}