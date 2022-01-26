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
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;

namespace PlannerApp.Components;

public partial class PlanForm
{
    [Inject]
    public IPlansService PlansService { get; set; }

    private PlanDetail _model = new PlanDetail();
    private bool _isBusy = false;
    private Stream _stream = null;
    private string _fileName = string.Empty;
    private string _errorMessage = string.Empty;

    private async Task SubmitFormAsync()
    {
        _errorMessage = "Done";
    }

    private async Task OnChooseFileAsync(InputFileChangeEventArgs e)
    {
        _errorMessage = string.Empty;

        var file = e.File;
        if (file != null)
        {
            if (file.Size > 2097152)
            {
                _errorMessage = "The file must be less than 2MB";
                return;
            }

            string[] allowedExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };

            string extension = Path.GetExtension(file.Name).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                _errorMessage = "Invalid Image File";
                return;
            }

            using (var stream = file.OpenReadStream(2097152))
            {
                var buffer = new byte[file.Size];
                await stream.ReadAsync(buffer, 0, (int)file.Size);
                _stream = new MemoryStream(buffer);
                _stream.Position = 0;
                _fileName = file.Name;
            }
        }
    }
}
