using System;
using AutoMapper;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using MetricsAgent.Requests;

namespace MetricsAgent.Settings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Профили для мапинга CPU метрик
            CreateMap<CpuMetricDto, CpuMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<CpuMetric, CpuMetricDto>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
         
            CreateMap<CpuMetricCreateRequest, CpuMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<CpuMetric, CpuMetricCreateRequest>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга .Net метрик
            CreateMap<DotNetMetricDto, DotNetMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<DotNetMetric, DotNetMetricDto>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<DotNetMetric, DotNetMetricCreateRequest>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга HDD метрик
            CreateMap<HddMetricDto, HddMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<HddMetric, HddMetricDto>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            CreateMap<HddMetricCreateRequest, HddMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<HddMetric, HddMetricCreateRequest>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга Network метрик
            CreateMap<NetworkMetricDto, NetworkMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<NetworkMetric, NetworkMetricDto>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            CreateMap<NetworkMetricCreateRequest, NetworkMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<NetworkMetric, NetworkMetricCreateRequest>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));

            // Профили для мапинга RAM метрик
            CreateMap<RamMetricDto, RamMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<RamMetric, RamMetricDto>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
            
            CreateMap<RamMetricCreateRequest, RamMetric>().ForMember(dbModel => dbModel.Time,
                o => o.MapFrom(t => t.Time.ToUnixTimeSeconds()));
            CreateMap<RamMetric, RamMetricCreateRequest>().ForMember(tm => tm.Time,
                time => time.MapFrom(t => DateTimeOffset.FromUnixTimeSeconds(t.Time)));
        }
    }
}
