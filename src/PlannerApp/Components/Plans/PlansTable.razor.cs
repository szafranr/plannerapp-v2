using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using PlannerApp;
using PlannerApp.Shared;
using PlannerApp.Components;
using MudBlazor;
using Blazored.FluentValidation;
using PlannerApp.Shared.Models;
using PlannerApp.Client.Services.Interfaces;
using static MudBlazor.CategoryTypes;

namespace PlannerApp.Components;

public partial class PlansTable
{
    [Inject]
    public IPlansService PlansService { get; set; }

    [Parameter]
    public EventCallback<PlanSummary> OnViewClicked { get; set; }

    [Parameter]
    public EventCallback<PlanSummary> OnDeleteClicked { get; set; }

    [Parameter]
    public EventCallback<PlanSummary> OnEditClicked { get; set; }

    private string _query = string.Empty;
    private MudTable<PlanSummary> _table;

    private async Task<TableData<PlanSummary>> ServerReloadAsync(TableState state)
    {
        try
        {
            var result = await PlansService.GetPlansAsync(_query, state.Page, state.PageSize);

            return new TableData<PlanSummary>
            {
                Items = result.Value.Records,
                TotalItems = result.Value.ItemsCount
            };
        }
        catch (Exception ex)
        {
            //Error.HandleError(ex);
        }

        return new TableData<PlanSummary>
        {
            Items = new List<PlanSummary>(),
            TotalItems = 0
        };
    }

    private void OnSearch(string query)
{
        _query = query;
        _table.ReloadServerData();
    }
}
