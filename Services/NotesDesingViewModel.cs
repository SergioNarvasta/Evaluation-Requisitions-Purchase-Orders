namespace HDProjectWeb.Services
{
    public class NotesDesingViewModel
    {
        /* 
          {}
        -- INPUT PARA SUBIR ARCHIVO 
        <div class="form-group" style="width:300px">               
                    <div class="input-group d-flex flex-row" >
                        <label class="input-group-btn">
                            <span class="btn btn-file" >
                               <input accept=".jpg,.png,.jpeg,.gif" class="hidden" name="banner" type="file" id="banner" style="width:90px">
                            </span>
                        </label>
                        <input class="form-control" id="banner_captura" readonly="readonly" name="banner_captura" type="text" value="" style="width:100px">
                    </div>
          </div> 

                //replace(/\s+/g, '') elimina todos los espacios en JS


          Clases de Bootstrap para diseño de interfaces
           d-flex flex-row    ->> alineamiento horizontal
           me-5 ms-2 ms-xl-5  ->> Margen e(end) left, s(start) right
           form-control-sm    ->> Tamaño de objeto small
           col-sm-4           ->> Ancho de objeto small
        <div class="form-group d-flex flex-row ms-xl-5"> Width en obj textbox


         <!-- 1ER DIV HORIZONTAL-->
            <div class="d-flex flex-row">
                <div class="form-group d-flex flex-row me-3">
                    <label class="me-2">Requerimiento</label>
                    <input asp-for="Rco_numero" class="form-control form-control-sm col-sm-4 " />
                    <span asp-validation-for="Rco_numero" class="text-danger"></span>
                </div>
                <div class="form-group d-flex flex-row ms-xl-5">
                    <label class="ms-xl-5">Fecha_Reg</label>
                    <input asp-for="Rco_fec_registro" class="form-control form-control-sm ms-2" />
                    <span asp-validation-for="Rco_fec_registro" class="text-danger"></span>
                </div>
                <div class="d-flex flex-row ms-xl-5">
                    <span>Estado </span>
                    <select name="cboEstado" class="ms-2">
                        <option value=1>VIGENTE</option>
                        <option value=0>ANULADO</option>
                    </select>
                </div>
                <div class="d-flex flex-row ms-xl-5">
                    <span>Tipo_Req </span>
                    <select name="cboEstado" class="ms-2">
                        <option value=1>COMPRA REGULAR</option>
                        <option value=2>CERTIF. CONFORMIDAD</option>
                        <option value=3>ADENDA CONTRATO</option>
                        <option value=4>CLS CONSULTOR</option>
                        <option value=5>CLS EMPRESA</option>
                        <option value=6>AM CONSULTOR</option>
                        <option value=7>AM EMPRESA</option>
                    </select>
                </div>
                <div class="d-flex flex-row ms-xl-5">
                    <span>Prioridad </span>
                    <select name="cboEstado" class="ms-2">
                        <option value=1>MUY ALTA</option>
                        <option value=2>ALTA</option>
                        <option value=3>MEDIA</option>
                        <option value=4>BAJA</option>
                    </select>
                </div>          
            </div> <br />   
            <!-- 2DO DIV HORIZONTAL-->
            <div class="d-flex flex-row">
                <div class="form-group d-flex flex-row ">
                    <label >U.Negocio</label>
                    <select asp-for="U_negocio" name="cboEstado" class="ms-2">
                        <option value=1>MINING & METALURGY    </option>
                        <option value=2>INFRAESTRUCTURE E&W   </option>
                        <option value=3>O&G-FEED & ENGINEERING</option>
                        <option value=4>SHARED SERVICES</option>    
                        <option value=5>O&G - PROJECTS </option>
                        <option value=6>M&M - E&G      </option>
                        <option value=7>ANTAMINA FASE VII</option>
                    </select>
                    <span asp-validation-for="U_negocio" class="text-danger"></span>
                </div>
                <div class="form-group d-flex flex-row ms-2 me-5">
                    <label asp-for="Centro_costo" class="control-label"></label>
                    <input asp-for="Centro_costo" class="form-control col-sm-1 ms-2" />
                    <span asp-validation-for="Centro_costo" class="text-danger"></span>
                </div>
                <div class="form-group d-flex flex-row ms-xl-5">
                    <label>Situacion</label>
                    <select asp-for="Rco_situacion_aprobado" id="CboSituacion" class="ms-2">
                        <option value=1>PENDIENTE</option>
                        <option value=2>APROBADO </option>
                        <option value=2>RECHAZADO</option>
                    </select>
                    <span asp-validation-for="Rco_situacion_aprobado" class="text-danger"></span>
                </div>
                <div class="form-group d-flex flex-row ms-5 me-5">
                    <label class="control-label me-2">Solicitud</label>
                    <input asp-for="User_solicita" class="form-control form-control-sm col-sm-4" />
                    <span asp-validation-for="User_solicita" class="text-danger"></span>
                </div>
            </div> <br />
            <!--3ER DIV HORIZONTAL-->
            <div class="d-flex flex-row">
                <div class="form-group d-flex flex-row me-5">
                    <label asp-for="User_solicita" class="control-label me-2"></label>
                    <input asp-for="User_solicita" class="form-control form-control-sm col-sm-4" />
                    <span asp-validation-for="User_solicita" class="text-danger"></span>
                </div>
                <div class="form-group d-flex flex-row ms-5 me-5">
                    <label  class="control-label me-2">Resumen</label>
                    <input asp-for="User_solicita" class="form-control form-control-sm col-sm-4" />
                    <span asp-validation-for="User_solicita" class="text-danger"></span>
                </div>


            </div>
            


                           <tr>
                                <td>01</td>
                                <td>00 </td>
                                <td>GENERAL                                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>10 </td>
                                <td>Management/Executive                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>11 </td>
                                <td>VP, Corporate Functions                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>12 </td>
                                <td>VP and General Manager                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>13 </td>
                                <td>VP Operations                                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>21 </td>
                                <td>Marketing and Business Development                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>22 </td>
                                <td>Risk and Insurance                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>23 </td>
                                <td>Accounting, Audit                                                               </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>24 </td>
                                <td>Secretarial and Administrative Support                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>25 </td>
                                <td>Human Resources                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>27 </td>
                                <td>Legal and Paralegal                                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>28 </td>
                                <td>Quality Management                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>29 </td>
                                <td>Corporate Systems Management                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2A </td>
                                <td>Administrative Services                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2B </td>
                                <td>Library                                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2C </td>
                                <td>Customer Support                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2D </td>
                                <td>Realty Services                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2E </td>
                                <td>Reception                                                                       </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2F </td>
                                <td>Financing, Financial Analysis, Treasury and Taxation                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2G </td>
                                <td>Document Processing and Desktop Publishing                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2H </td>
                                <td>Facilities Management                                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2J </td>
                                <td>Risk Evaluation/Analysis/Management                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2L </td>
                                <td>ADMINISTRATION                                                                  </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2O </td>
                                <td>OVERHEAD - PROYECTOS                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2P </td>
                                <td>Payroll                                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2R </td>
                                <td>Public Relations and Communications                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2S </td>
                                <td>Training                                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2T </td>
                                <td>Translation                                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>2Z </td>
                                <td>HEALTH, SAFETY &amp; ENVIRONMENT (ADMINISTRATION)                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>30 </td>
                                <td>Project Management                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>31 </td>
                                <td>Project Administration                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>32 </td>
                                <td>Planning and Scheduling                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>33 </td>
                                <td>General Estimating                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>34 </td>
                                <td>Cost Control                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>35 </td>
                                <td>Project Cost Accounting                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>36 </td>
                                <td>System Administration                                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>37 </td>
                                <td>Document Control                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>38 </td>
                                <td>Quality Assurance / Validation - Projects                                       </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>39 </td>
                                <td>Project Control                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3C </td>
                                <td>Civil and others Estimating                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3E </td>
                                <td>Electrical/Instrumentation Estimating                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3M </td>
                                <td>Mechanical Estimating                                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3P </td>
                                <td>Piping Estimating                                                               </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3R </td>
                                <td>RISK MANAGEMENT (CONSTRUCTION)                                                  </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3X </td>
                                <td>OTHER PM/PC (PROJECT SPECIFIC)                                                  </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>3Z </td>
                                <td>HEALTH, SAFETY AND ENVIRONMENT (PROJECT MANAGEMENT)                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>40 </td>
                                <td>Engineering Management                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>41 </td>
                                <td>Civil                                                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>42 </td>
                                <td>Concrete                                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>43 </td>
                                <td>Structural                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>44 </td>
                                <td>Architecture                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>45 </td>
                                <td>Mechanical                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>46 </td>
                                <td>Piping                                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>47 </td>
                                <td>Electrical                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>48 </td>
                                <td>Automation, I&amp;C                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>49 </td>
                                <td>Process                                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4A </td>
                                <td>Agriculture/Agronomy                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4B </td>
                                <td>Building Services/HVAC                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4C </td>
                                <td>Telecommunications                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4D </td>
                                <td>Industrial Engineering                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4E </td>
                                <td>Environment/Biology                                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4F </td>
                                <td>Drilling                                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4G </td>
                                <td>Geotechnical                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4H </td>
                                <td>Hydraulic/Hydrology                                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4I </td>
                                <td>Support to Computer Assisted Design                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4J </td>
                                <td>Geology                                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4K </td>
                                <td>Economics, Social Studies                                                       </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4L </td>
                                <td>Layout                                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4M </td>
                                <td>Mining                                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4N </td>
                                <td>Metallurgy                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4P </td>
                                <td>Port/Marine Engineering                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4Q </td>
                                <td>Pipelines                                                                       </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4R </td>
                                <td>Railroad Engineering                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4S </td>
                                <td>Simulation                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4T </td>
                                <td>Transportation Engineering                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4U </td>
                                <td>Urbanism                                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4V </td>
                                <td>Safety Processes                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4W </td>
                                <td>HYDROGEOLOGY                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4X </td>
                                <td>OTHER ENGINEERING                                                               </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4Y </td>
                                <td>Fire &amp; Gas                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>4Z </td>
                                <td>Surveying, Geodesy and Geography                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>50 </td>
                                <td>Procurement Management                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>51 </td>
                                <td>Contract Administration                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>52 </td>
                                <td>Purchasing                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>53 </td>
                                <td>Expediting                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>54 </td>
                                <td>Material Control                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>55 </td>
                                <td>Logistic and Transportation                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>56 </td>
                                <td>General Quality Control                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>57 </td>
                                <td>Warehousing/Inventory Control                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5B </td>
                                <td>Quality Control Concrete/Structural                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5C </td>
                                <td>Quality Control Civil                                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5E </td>
                                <td>Quality Control Electrical                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5M </td>
                                <td>Quality Control Mechanical                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5P </td>
                                <td>Quality Control Piping/Pressure Vessels                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5W </td>
                                <td>Quality Control Welding                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>5X </td>
                                <td>OTHER PROCUREMENT (PROJECT SPECIFIC)                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>60 </td>
                                <td>Construction Management                                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>61 </td>
                                <td>Construction Administration                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>62 </td>
                                <td>Construction Supervision (General)                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>63 </td>
                                <td>Construction Planning &amp; Cost Control                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>64 </td>
                                <td>Field Engineering and Inspection                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>65 </td>
                                <td>FIELD CONTRACTS ADMIN &amp; PROCUREMENT                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>66 </td>
                                <td>Labour Relations                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>67 </td>
                                <td>Site Security                                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>68 </td>
                                <td>Health, Safety and Environment - Projects                                       </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>69 </td>
                                <td>Site Environment                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6A </td>
                                <td>Architectural Construction Supervision                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6C </td>
                                <td>Concrete/Civil Construction Supervision                                         </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6E </td>
                                <td>Electrical Construction Supervision                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6M </td>
                                <td>Mechanical Construction Supervision                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6P </td>
                                <td>Piping Construction Supervision                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6S </td>
                                <td>Structural Construction Supervision                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6X </td>
                                <td>OTHER CONSTRUCTION (PROJECT SPECIFIC)                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>6Y </td>
                                <td>QUALITY ON SITE                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>70 </td>
                                <td>Commissioning Management and Administration                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>75 </td>
                                <td>Mechanical Commissioning                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>76 </td>
                                <td>Piping Commissioning                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>77 </td>
                                <td>Electrical Commissioning                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>78 </td>
                                <td>Automation Commissioning                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>7S </td>
                                <td>Commissioning Warranty/Safety                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>7T </td>
                                <td>Commissioning Turnover                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>7X </td>
                                <td>OTHER COMMISSIONING (PROJECT SPECIFIC)                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>80 </td>
                                <td>O&amp;M Management and Administration                                               </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>81 </td>
                                <td>Operation                                                                       </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>82 </td>
                                <td>Maintenance                                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>8X </td>
                                <td>OTHER O&amp;M (PROJECT SPECIFIC)                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>90 </td>
                                <td>Management, GIT                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>91 </td>
                                <td>Analysis, GIT                                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>93 </td>
                                <td>Analysis and Programming, GIT                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>95 </td>
                                <td>Programming, GIT                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>97 </td>
                                <td>Architecture, GIT                                                               </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>99 </td>
                                <td>Database                                                                        </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9A </td>
                                <td>Electronic Mail                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9B </td>
                                <td>Telecommunication, GIT                                                          </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9C </td>
                                <td>Thin Client                                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9D </td>
                                <td>Operations Systems                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9E </td>
                                <td>Business Solutions                                                              </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9F </td>
                                <td>Workstation &amp; Distribution                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9G </td>
                                <td>User Support                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9H </td>
                                <td>Data Storage                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9J </td>
                                <td>Production, GIT                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9K </td>
                                <td>Method &amp; Tools                                                                  </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9L </td>
                                <td>System Security                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9M </td>
                                <td>IT Project Management                                                           </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9Y </td>
                                <td>CONTRACT ADMINISTRATION &amp; LOGISTICS                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>9Z </td>
                                <td>CONTRACT ADMINISTRATION &amp; LOGISTICS                                             </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>G1 </td>
                                <td>MANAGEMENT                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>L1 </td>
                                <td>LEAD CIVIL                                                                      </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>L5 </td>
                                <td>LEAD MECHANICAL                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>L6 </td>
                                <td>LEAD PIPING                                                                     </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>L7 </td>
                                <td>LEAD ELECTRICAL                                                                 </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>L8 </td>
                                <td>LEAD INSTRUMENTATION                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>L9 </td>
                                <td>LEAD PROCESSES                                                                  </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>LE </td>
                                <td>LEAD ENVIRONMENT                                                                </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>LF </td>
                                <td>LEAD FINANCE                                                                    </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>LG </td>
                                <td>LEAD GEOTECHNICAL                                                               </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>LS </td>
                                <td>LEAD HEALTH &amp; SAFETY                                                            </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>N8 </td>
                                <td>INTERMEDIO DE ABASTECIMIENTO,                                                   </td>
                            </tr>
                            <tr>
                                <td>01</td>
                                <td>SU </td>
                                <td>POR REGULARIZAR                                                                 </td>
                            </tr>
         */


    }
}
